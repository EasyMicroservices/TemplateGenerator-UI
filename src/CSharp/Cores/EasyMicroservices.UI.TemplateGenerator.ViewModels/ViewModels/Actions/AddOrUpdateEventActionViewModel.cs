using EasyMicroservices.UI.Cores;
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

    string _EffectiveVariableName;
    public string EffectiveVariableName
    {
        get => _EffectiveVariableName;
        set
        {
            _EffectiveVariableName = value;
            OnPropertyChanged(nameof(EffectiveVariableName));
        }
    }

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

    FormItemContract _SelectedFormItem;
    public FormItemContract SelectedFormItem
    {
        get => _SelectedFormItem;
        set
        {
            if (_SelectedFormItem == value)
                return;
            _SelectedFormItem = value;
            OnPropertyChanged(nameof(SelectedFormItem));
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
        SelectedFormItem = null;
        EventActionsListViewModel?.Children?.Clear();
    }

    public void SelectForUpdate(FormItemEventActionContract update)
    {
        EffectiveVariableName = update?.InfluencedToVariableName;
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
            FormItemId = SelectedFormItem?.Id,
            InfluencedToVariableName = EffectiveVariableName,
            Children = EventActionsListViewModel.Children.ToList(),
        };
    }
}