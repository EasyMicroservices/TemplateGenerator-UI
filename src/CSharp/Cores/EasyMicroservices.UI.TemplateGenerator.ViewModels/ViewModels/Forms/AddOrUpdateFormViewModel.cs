using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Forms
{
    public class AddOrUpdateFormViewModel : BaseViewModel
    {
        public AddOrUpdateFormViewModel(FormClient formClient)
        {
            _formClient = formClient;
            SaveCommand = new TaskRelayCommand(this, Save);
            DeleteCommand = new RelayCommand<FormItemContract>(Delete);
            Clear();
        }

        public TaskRelayCommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public Action<FormItemContract> OnDelete { get; set; }
        public Action OnSuccess { get; set; }

        readonly FormClient _formClient;
        FormContract _UpdateFormContract;
        /// <summary>
        /// for update
        /// </summary>
        public FormContract UpdateFormContract
        {
            get
            {
                return _UpdateFormContract;
            }
            set
            {
                if (value is not null)
                {
                    Name = value.Name;
                    if (value.Items != null)
                    {
                        FormItems.Clear();
                        foreach (var item in value.Items)
                        {
                            FormItems.Add(item);
                        }
                    }
                }
                _UpdateFormContract = value;
                OnPropertyChanged(nameof(UpdateFormContract));
            }
        }
        public FormItemContract SelectFormItemContract { get; set; }
        public ObservableCollection<FormItemContract> FormItems { get; set; } = new ObservableCollection<FormItemContract>();

        string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public async Task Save()
        {
            if (UpdateFormContract is not null)
                await UpdateForm();
            else
                await AddForm();
            OnSuccess?.Invoke();
        }

        public async Task AddForm()
        {
            await _formClient.AddAsync(new CreateFormRequestContract()
            {
                Name = Name,
                FormItems = FormItems
            }).AsCheckedResult(x => x.Result);
            Clear();
        }

        public async Task UpdateForm()
        {
            await _formClient.UpdateChangedValuesOnlyAsync(new FormContract()
            {
                Id = UpdateFormContract.Id,
                Name = Name,
                Items = FormItems
            }).AsCheckedResult(x => x.Result);
            Clear();
        }


        T GetCurrentProperty<T>(Func<FormContract, T> func)
        {
            return UpdateFormContract == null ? default : func(UpdateFormContract);
        }

        public async Task LoadConfig()
        {

        }

        public void Clear()
        {
            Name = "";
            UpdateFormContract = default;
        }

        private void Delete(FormItemContract contract)
        {
            FormItems.Remove(contract);
            OnDelete(contract);
        }
    }
}
