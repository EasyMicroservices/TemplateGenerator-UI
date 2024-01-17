using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Interfaces;
using System.Collections.ObjectModel;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Actions;
public class ActionsViewModel : BaseViewModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="actionClient"></param>
    public ActionsViewModel(ActionClient actionClient)
    {
        _actionClient = actionClient;
        RefreshCommand = new TaskRelayCommand(this, Refresh);
        RefreshCommand.Execute(null);
    }

    public IAsyncCommand RefreshCommand { get; set; }

    readonly ActionClient _actionClient;
    ActionContract _SelectedAction;
    public ActionContract SelectedAction
    {
        get => _SelectedAction;
        set
        {
            if (_SelectedAction == value)
                return;
            _SelectedAction = value;
            OnPropertyChanged(nameof(SelectedAction));
        }
    }

    public int Index { get; set; } = 0;
    public int Length { get; set; } = 50;
    public int TotalCount { get; set; }
    public ObservableCollection<ActionContract> Actions { get; set; } = new ObservableCollection<ActionContract>();

    public async Task Refresh()
    {
        var filteredResult = await _actionClient.FilterAsync(new FilterRequestContract()
        {
            IsDeleted = false,
            Index = Index,
            Length = Length,
        }).AsCheckedResult(x => (x.Result, x.TotalCount));

        Actions.Clear();
        TotalCount = (int)filteredResult.TotalCount;
        foreach (var form in filteredResult.Result)
        {
            Actions.Add(form);
        }
        OnGetActions.TrySetResult();
    }

    TaskCompletionSource OnGetActions = new TaskCompletionSource();

    public async Task<List<ActionContract>> OnGetActionsComplete()
    {
        await Task.WhenAny(OnGetActions.Task, Task.Delay(TimeSpan.FromSeconds(5)));
        return Actions.ToList();
    }
}