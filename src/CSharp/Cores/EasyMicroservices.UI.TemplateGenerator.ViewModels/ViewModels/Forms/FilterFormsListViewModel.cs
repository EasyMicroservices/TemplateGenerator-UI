using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.Cores.Interfaces;
using TemplateGenerators.GeneratedServices;
using System.Collections.ObjectModel;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Forms
{
    public class FilterFormsListViewModel : BaseViewModel
    {
        public FilterFormsListViewModel(FormClient formClient)
        {
            _formClient = formClient;
            SearchCommand = new TaskRelayCommand(this, Search);
            DeleteCommand = new TaskRelayCommand<FormContract>(this, Delete);
            SearchCommand.Execute(null);
        }

        public IAsyncCommand SearchCommand { get; set; }
        public IAsyncCommand DeleteCommand { get; set; }

        public Action<FormContract> OnDelete { get; set; }
        readonly FormClient _formClient;
        FormContract _SelectedFormContract;
        public FormContract SelectedFormContract
        {
            get => _SelectedFormContract;
            set
            {
                _SelectedFormContract = value;
                OnPropertyChanged(nameof(SelectedFormContract));
            }
        }

        public int Index { get; set; } = 0;
        public int Length { get; set; } = 10;
        public int TotalCount { get; set; }
        public string SortColumnNames { get; set; }
        public ObservableCollection<FormContract> Forms { get; set; } = new ObservableCollection<FormContract>();

        public async Task Search()
        {
            var filteredResult = await _formClient.FilterAsync(new FilterRequestContract()
            {
                IsDeleted = false,
                Index = Index,
                Length = Length,
            }).AsCheckedResult(x => (x.Result, x.TotalCount));

            Forms.Clear();
            TotalCount = (int)filteredResult.TotalCount;
            foreach (var form in filteredResult.Result)
            {
                Forms.Add(form);
            }
        }

        public async Task Delete(FormContract contract)
        {
            await _formClient.SoftDeleteByIdAsync(new Int64SoftDeleteRequestContract()
            {
                Id = contract.Id,
                IsDelete = true
            }).AsCheckedResult(x => x);
            Forms.Remove(contract);
            OnDelete?.Invoke(contract);
        }
    }
}

