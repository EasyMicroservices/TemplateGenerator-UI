using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.TemplateGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    FormItemEventActionContract _CurrentFormItemAction;
    public FormItemEventActionContract CurrentFormItemAction
    {
        get
        {
            return _CurrentFormItemAction;
        }
        set
        {
            _CurrentFormItemAction = value;
            if (value != null)
            {
                Children.Clear();
                foreach (var item in value.Children)
                {
                    Children.Add(item);
                }
                IndexOrderingActions.ReOrderIndexes();
            }
            OnPropertyChanged(nameof(CurrentFormItemAction));
        }
    }

    public FormItemEventActionContract SelectedFormItemEventAction { get; set; }

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

    public void DoDeleteSelected()
    {
        if (SelectedFormItemEventAction != null)
        {
            Children.Remove(SelectedFormItemEventAction);
        }
    }
}