using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Interfaces;
using EasyMicroservices.UI.TemplateGenerator.Helpers;
using EasyMicroservices.UI.TemplateGenerator.ViewModels.Actions;
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
    }

    public IAsyncCommand RefreshCommand { get; set; }

    public EventActionsListViewModel EventActionsListViewModel { get; set; }
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

    public void Clear()
    {
        SelectedEvent = null;
        EventActionsListViewModel?.Children?.Clear();
    }

    public void SelectForUpdate(FormItemEventContract update)
    {
        EventActionsListViewModel.Children.Clear();
        foreach (var item in update.FormItemEventActions)
        {
            EventActionsListViewModel.Children.Add(item);
        }
        EventActionsListViewModel.OnPropertyChanged(nameof(EventActionsListViewModel.Children));
    }

    public FormItemEventContract GetFormItemEvent(FormItemEventContract update)
    {
        var result = GetFormItemEvent();
        result.Id = update.Id;
        return result;
    }

    public FormItemEventContract GetFormItemEvent()
    {
        return new FormItemEventContract()
        {
            Event = SelectedEvent,
            EventId = SelectedEvent.Id,
            FormItemEventActions = EventActionsListViewModel.Children.ToList(),
        };
    }
}