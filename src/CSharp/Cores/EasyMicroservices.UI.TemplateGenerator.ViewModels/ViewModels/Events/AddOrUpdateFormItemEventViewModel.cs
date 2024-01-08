using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Interfaces;
using EasyMicroservices.UI.TemplateGenerator.Helpers;
using System.Collections.ObjectModel;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Events;
public class AddOrUpdateFormItemEventViewModel : BaseViewModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="actionClient"></param>
    public AddOrUpdateFormItemEventViewModel(ActionClient actionClient)
    {
        _actionClient = actionClient;
        RefreshCommand = new TaskRelayCommand(this, Refresh);
        RefreshCommand.Execute(null);
        IndexOrderingActions = new IndexOrderingCollection<FormItemEventActionContract>(EventActions, (x, i) => x.OrderIndex = i);
    }

    public IAsyncCommand RefreshCommand { get; set; }

    readonly ActionClient _actionClient;
    EventContract _SelectedEvent;
    public EventContract SelectedEvent
    {
        get => _SelectedEvent;
        set
        {
            if (_SelectedEvent == value)
                return;
            _SelectedEvent = value;
            OnPropertyChanged(nameof(SelectedEvent));
        }
    }

    public int Index { get; set; } = 0;
    public int Length { get; set; } = 50;
    public int TotalCount { get; set; }
    public ObservableCollection<FormItemEventActionContract> EventActions { get; set; } = new ObservableCollection<FormItemEventActionContract>();
    public IndexOrderingCollection<FormItemEventActionContract> IndexOrderingActions { get; }

    public async Task Refresh()
    {
        //var filteredResult = await _actionClient.FilterAsync(new FilterRequestContract()
        //{
        //    IsDeleted = false,
        //    Index = Index,
        //    Length = Length,
        //}).AsCheckedResult(x => (x.Result, x.TotalCount));

        //Actions.Clear();
        //TotalCount = (int)filteredResult.TotalCount;
        //foreach (var form in filteredResult.Result)
        //{
        //    Actions.Add(form);
        //}
    }

    public FormItemEventContract GetFormItemEvent()
    {
        return new FormItemEventContract()
        {
            Event = SelectedEvent,
            EventId = SelectedEvent.Id,
            FormItemEventActions = EventActions,
        };
    }
}