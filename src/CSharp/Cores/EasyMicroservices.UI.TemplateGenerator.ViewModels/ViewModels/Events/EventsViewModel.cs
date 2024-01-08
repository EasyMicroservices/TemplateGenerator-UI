using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Interfaces;
using System.Collections.ObjectModel;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Events;
public class EventsViewModel : BaseViewModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventClient"></param>
    public EventsViewModel(EventClient eventClient)
    {
        _eventClient = eventClient;
        RefreshCommand = new TaskRelayCommand(this, Refresh);
        RefreshCommand.Execute(null);
    }

    public IAsyncCommand RefreshCommand { get; set; }

    readonly EventClient _eventClient;
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
    public ObservableCollection<EventContract> Events { get; set; } = new ObservableCollection<EventContract>();

    public async Task Refresh()
    {
        var filteredResult = await _eventClient.FilterAsync(new FilterRequestContract()
        {
            IsDeleted = false,
            Index = Index,
            Length = Length,
        }).AsCheckedResult(x => (x.Result, x.TotalCount));

        Events.Clear();
        TotalCount = (int)filteredResult.TotalCount;
        foreach (var form in filteredResult.Result)
        {
            Events.Add(form);
        }
    }
}