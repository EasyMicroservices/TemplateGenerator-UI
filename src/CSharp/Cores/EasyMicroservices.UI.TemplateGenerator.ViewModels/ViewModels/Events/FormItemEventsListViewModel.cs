using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Interfaces;
using System.Collections.ObjectModel;
using FormItemEventContract = TemplateGenerators.GeneratedServices.FormItemEventContract;
namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Events;

public class FormItemEventsListViewModel : BaseViewModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="actionClient"></param>
    public FormItemEventsListViewModel()
    {
        //_actionClient = actionClient;
        RefreshCommand = new TaskRelayCommand(this, Refresh);
        RefreshCommand.Execute(null);
    }

    public IAsyncCommand RefreshCommand { get; set; }

    //readonly ActionClient _actionClient;
    FormItemEventContract _SelectedFormItemEvent;
    public FormItemEventContract SelectedFormItemEvent
    {
        get => _SelectedFormItemEvent;
        set
        {
            _SelectedFormItemEvent = value;
            OnPropertyChanged(nameof(SelectedFormItemEvent));
        }
    }

    public int Index { get; set; } = 0;
    public int Length { get; set; } = 50;
    public int TotalCount { get; set; }
    public ObservableCollection<FormItemEventContract> FormItemEvents { get; set; } = new ObservableCollection<FormItemEventContract>();

    public async Task Refresh()
    {
        //var filteredResult = await _actionClient.FilterAsync(new FilterRequestContract()
        //{
        //    IsDeleted = false,
        //    Index = Index,
        //    Length = Length,
        //}).AsCheckedResult(x => (x.Result, x.TotalCount));

        //FormItemEvents.Clear();
        //TotalCount = (int)filteredResult.TotalCount;
        //foreach (var item in filteredResult.Result)
        //{
        //    FormItemEvents.Add(item);
        //}
    }

    public MessageContract DoDelete(FormItemEventContract item)
    {
        FormItemEvents.Remove(item);
        return true;
    }

    public MessageContract Add(FormItemEventContract item)
    {
        FormItemEvents.Add(item);
        return true;
    }

    public MessageContract Update(FormItemEventContract oldItem, FormItemEventContract newItem)
    {
        FormItemEvents.Remove(oldItem);
        FormItemEvents.Insert(0, newItem);
        return true;
    }
}