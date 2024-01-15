using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.TemplateGenerator.Helpers;
using System.Collections.ObjectModel;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Actions;
public class AddOrUpdateEventActionViewModel : BaseViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public AddOrUpdateEventActionViewModel()
    {
    }

    public EventActionsListViewModel EventActionsListViewModel { get; set; }

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

    public FormItemEventActionContract SelectedFormItemEventAction { get; set; }

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
        SelectedAction = null;
        EventActionsListViewModel?.Children?.Clear();
    }

    public void SelectForUpdate(FormItemEventActionContract update)
    {
        EventActionsListViewModel.Children.Clear();
        foreach (var item in update.Children)
        {
            EventActionsListViewModel.Children.Add(item);
        }
        EventActionsListViewModel.OnPropertyChanged(nameof(EventActionsListViewModel.Children));
    }

    public FormItemEventActionContract GetEventAction(FormItemEventActionContract update)
    {
        var result = GetEventAction();
        result.Id = update.Id;
        return result;
    }

    public FormItemEventActionContract GetEventAction()
    {
        return new FormItemEventActionContract()
        {
            Action = SelectedAction,
            ActionId = SelectedAction.Id,
            Children = EventActionsListViewModel.Children.ToList(),
        };
    }
}