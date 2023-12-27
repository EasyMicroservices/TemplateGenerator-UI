using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.FormItems;
public class AddOrUpdateFormItemViewModel : BaseViewModel
{
    public AddOrUpdateFormItemViewModel()
    {
        SaveCommand = new TaskRelayCommand(this, Save);
        DeleteCommand = new RelayCommand<FormItemContract>(Delete);
        Clear();
    }

    public TaskRelayCommand SaveCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    public Action<FormItemContract> OnDelete { get; set; }
    public Action<FormItemContract> OnSuccess { get; set; }
    FormItemContract _UpdateFormItemContract;
    /// <summary>
    /// for update
    /// </summary>
    public FormItemContract UpdateFormItemContract
    {
        get
        {
            return _UpdateFormItemContract;
        }
        set
        {
            if (value is not null)
            {
                Title = value.Title;
                Type = value.Type;
                DefaultValue = value.DefaultValue;
                if (value.Items != null)
                {
                    foreach (var item in value.Items)
                    {
                        FormItems.Add(item);
                    }
                }
            }
            _UpdateFormItemContract = value;
            OnPropertyChanged(nameof(UpdateFormItemContract));
        }
    }

    public ObservableCollection<FormItemContract> FormItems { get; set; } = new ObservableCollection<FormItemContract>();

    string _Title;
    public string Title
    {
        get => _Title;
        set
        {
            _Title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    ItemType _Type = ItemType.TextBox;
    public ItemType Type
    {
        get => _Type;
        set
        {
            _Type = value;
            OnPropertyChanged(nameof(Type));
        }
    }

    string _DefaultValue;

    public string DefaultValue
    {
        get => _DefaultValue;
        set
        {
            _DefaultValue = value;
            OnPropertyChanged(nameof(DefaultValue));
        }
    }

    public async Task Save()
    {
        OnSuccess?.Invoke(new FormItemContract()
        {
            Id = UpdateFormItemContract == null ? 0 : UpdateFormItemContract.Id,
            DefaultValue = DefaultValue,
            Type = Type,
            Title = Title,
            Items = FormItems
        });
    }

    T GetCurrentProperty<T>(Func<FormItemContract, T> func)
    {
        return UpdateFormItemContract == null ? default : func(UpdateFormItemContract);
    }

    public async Task LoadConfig()
    {

    }

    public void Clear()
    {
        Title = "";
        DefaultValue = "";
        FormItems.Clear();
        UpdateFormItemContract = default;
    }

    private void Delete(FormItemContract contract)
    {
        FormItems.Remove(contract);
        OnDelete(contract);
    }
}
