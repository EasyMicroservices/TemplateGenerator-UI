using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Commands;
using EasyMicroservices.UI.TemplateGenerator.Helpers;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using System.Windows.Input;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.FormItems;
public class AddOrUpdateFormItemViewModel : BaseViewModel
{
    public AddOrUpdateFormItemViewModel(NoParentFormItemClient noParentFormItemClient)
    {
        _noParentFormItemClient = noParentFormItemClient;
        SaveCommand = new TaskRelayCommand(this, Save);
        DeleteCommand = new RelayCommand<FormItemContract>(Delete);
        OrderingFormItems = new IndexOrderingCollection<FormItemContract>(FormItems, (x, i) => x.Index = i);
        Clear();
    }

    public TaskRelayCommand SaveCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    NoParentFormItemClient _noParentFormItemClient;

    public Action<FormItemContract> OnDelete { get; set; }
    public Action<FormItemContract> OnSuccess { get; set; }
    public FormItemContract DeleteFormItemContract { get; set; }


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
                if (value.PrimaryFormItemId.HasValue)
                    SelectedNoParentFormItem = NoParentFormItems.FirstOrDefault(x => x.Id == value.PrimaryFormItemId);
            }
            _UpdateFormItemContract = value;
            OnPropertyChanged(nameof(UpdateFormItemContract));
        }
    }

    public ObservableCollection<FormItemContract> FormItems { get; set; } = new ObservableCollection<FormItemContract>();
    public ObservableCollection<EventContract> Events { get; set; } = new ObservableCollection<EventContract>();
    public ObservableCollection<FormItemContract> NoParentFormItems { get; set; } = new ObservableCollection<FormItemContract>();

    public IndexOrderingCollection<FormItemContract> OrderingFormItems { get; }

    FormItemContract _SelectedNoParentFormItem;
    public FormItemContract SelectedNoParentFormItem
    {
        get => _SelectedNoParentFormItem;
        set
        {
            _SelectedNoParentFormItem = value;
            OnPropertyChanged(nameof(SelectedNoParentFormItem));
            OnPropertyChanged(nameof(IsDisabled));
        }
    }

    public bool IsDisabled
    {
        get
        {
            return SelectedNoParentFormItem != null;
        }
    }

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

    string _VariableName;
    public string VariableName
    {
        get => _VariableName;
        set
        {
            _VariableName = value;
            OnPropertyChanged(nameof(VariableName));
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

    public string GetTitle(FormItemContract formItemContract)
    {
        if (formItemContract.Title.IsNullOrEmpty())
            return formItemContract.PrimaryFormItem?.Title ?? GetLanguage("NoName!");
        return formItemContract.Title;
    }

    FormItemContract GetContract()
    {
        return new FormItemContract()
        {
            Id = UpdateFormItemContract == null ? 0 : UpdateFormItemContract.Id,
            DefaultValue = DefaultValue,
            Type = Type,
            Title = Title,
            Items = FormItems,
            PrimaryFormItemId = SelectedNoParentFormItem?.Id,
        };
    }

    CreateFormItemRequestContract GetCreateContract()
    {
        return new CreateFormItemRequestContract()
        {
            DefaultValue = DefaultValue,
            Type = Type,
            Title = Title,
            Items = JsonSerializer.Deserialize<List<CreateFormItemContract>>(JsonSerializer.Serialize(FormItems)),
            PrimaryFormItemId = SelectedNoParentFormItem?.Id,
        };
    }

    UpdateFormItemRequestContract GetUpdateContract()
    {
        return new UpdateFormItemRequestContract()
        {
            Id = UpdateFormItemContract == null ? 0 : UpdateFormItemContract.Id,
            DefaultValue = DefaultValue,
            Type = Type,
            Title = Title,
            Items = FormItems,
            PrimaryFormItemId = SelectedNoParentFormItem?.Id,
        };
    }

    public async Task Save()
    {
        OnSuccess?.Invoke(GetContract());
    }

    public async Task SaveNotParentToService()
    {
        if (UpdateFormItemContract == null || UpdateFormItemContract.Id == 0)
        {
            var addContract = GetCreateContract();
            var addResult = await _noParentFormItemClient.AddAsync(addContract);
            if (addResult.IsSuccess)
                OnSuccess?.Invoke(GetContract());
        }
        else
        {
            var addResult = await _noParentFormItemClient.UpdateChangedValuesOnlyAsync(GetUpdateContract());
            if (addResult.IsSuccess)
                OnSuccess?.Invoke(addResult.Result);
        }
    }

    public async Task LoadConfig()
    {
        var result = await _noParentFormItemClient.FilterAsync(new FilterRequestContract());
        if (result.IsSuccess)
        {
            foreach (var item in result.Result)
            {
                NoParentFormItems.Add(item);
            }
            if (UpdateFormItemContract != null && UpdateFormItemContract.PrimaryFormItemId.HasValue)
                SelectedNoParentFormItem = NoParentFormItems.FirstOrDefault(x => x.Id == UpdateFormItemContract.PrimaryFormItemId);
        }
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
