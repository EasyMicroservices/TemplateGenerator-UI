using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Interfaces;
using System.Collections.ObjectModel;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.FormItems;
public class FormItemsViewModel : BaseViewModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="formItemClient"></param>
    public FormItemsViewModel(NoParentFormItemClient formItemClient)
    {
        _formItemClient = formItemClient;
        RefreshCommand = new TaskRelayCommand(this, Refresh);
        RefreshCommand.Execute(null);
    }

    public IAsyncCommand RefreshCommand { get; set; }

    readonly NoParentFormItemClient _formItemClient;
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

    public int Index { get; set; } = 0;
    public int Length { get; set; } = 50;
    public int TotalCount { get; set; }
    public ObservableCollection<FormItemContract> FormItems { get; set; } = new ObservableCollection<FormItemContract>();

    public async Task Refresh()
    {
        var filteredResult = await _formItemClient.FilterAsync(new FilterRequestContract()
        {
            IsDeleted = false,
            Index = Index,
            Length = Length,
        }).AsCheckedResult(x => (x.Result, x.TotalCount));

        FormItems.Clear();
        TotalCount = (int)filteredResult.TotalCount;
        foreach (var form in filteredResult.Result)
        {
            FormItems.Add(form);
        }
        OnGetFormItems.TrySetResult();
    }

    TaskCompletionSource OnGetFormItems = new TaskCompletionSource();

    public async Task<List<FormItemContract>> OnGetFormItemsComeplete()
    {
        await Task.WhenAny(OnGetFormItems.Task, Task.Delay(TimeSpan.FromSeconds(5)));
        return FormItems.ToList();
    }
}