using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Interfaces;
using System.Collections.ObjectModel;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.NoParentFormItems;
public class FilterNoParentFormItemsListViewModel : BaseViewModel
{
    public FilterNoParentFormItemsListViewModel(NoParentFormItemClient noParentFormItemClient)
    {
        _noParentFormItemClient = noParentFormItemClient;
        SearchCommand = new TaskRelayCommand(this, Search);
        DeleteCommand = new TaskRelayCommand<FormItemContract>(this, Delete);
        SearchCommand.Execute(null);
    }

    public IAsyncCommand SearchCommand { get; set; }
    public IAsyncCommand DeleteCommand { get; set; }

    public Action<FormItemContract> OnDelete { get; set; }
    readonly NoParentFormItemClient _noParentFormItemClient;

    FormItemContract _SelectedFormItemContract;
    public FormItemContract SelectedFormItemContract
    {
        get => _SelectedFormItemContract;
        set
        {
            _SelectedFormItemContract = value;
            OnPropertyChanged(nameof(SelectedFormItemContract));
        }
    }

    public int Index { get; set; } = 0;
    public int Length { get; set; } = 10;
    public int TotalCount { get; set; }
    public string SortColumnNames { get; set; }
    public ObservableCollection<FormItemContract> Items { get; set; } = new ObservableCollection<FormItemContract>();

    public async Task Search()
    {
        var filteredResult = await _noParentFormItemClient.FilterAsync(new FilterRequestContract()
        {
            IsDeleted = false,
            Index = Index,
            Length = Length,
        }).AsCheckedResult(x => (x.Result, x.TotalCount));

        Items.Clear();
        TotalCount = (int)filteredResult.TotalCount;
        foreach (var form in filteredResult.Result)
        {
            Items.Add(form);
        }
    }

    public async Task Delete(FormItemContract contract)
    {
        await _noParentFormItemClient.SoftDeleteByIdAsync(new Int64SoftDeleteRequestContract()
        {
            Id = contract.Id,
            IsDelete = true
        }).AsCheckedResult(x => x);
        Items.Remove(contract);
        OnDelete?.Invoke(contract);
    }
}

