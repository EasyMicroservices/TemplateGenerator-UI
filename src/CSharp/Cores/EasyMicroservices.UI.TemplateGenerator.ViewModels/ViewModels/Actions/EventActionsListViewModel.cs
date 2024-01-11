using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.TemplateGenerator.Helpers;
using System.Collections.ObjectModel;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Actions;
public class EventActionsListViewModel : BaseViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public EventActionsListViewModel()
    {
        IndexOrderingActions = new IndexOrderingCollection<FormItemEventActionContract>(Children, (x, i) => x.OrderIndex = i);
    }

    public int Index { get; set; } = 0;
    public int Length { get; set; } = 50;
    public int TotalCount { get; set; }
    public ObservableCollection<FormItemEventActionContract> Children { get; set; } = new ObservableCollection<FormItemEventActionContract>();
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
}