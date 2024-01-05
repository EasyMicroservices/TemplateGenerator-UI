using EasyMicroservices.UI.Cores;
using System.Collections.ObjectModel;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Generators;
public class FormItemListGeneratorViewModel : BaseViewModel
{
    public FormItemListGeneratorViewModel()
    {

    }

    public ObservableCollection<FormItemContract> FormItems { get; set; } = new ObservableCollection<FormItemContract>();

    public async Task InitializeFormItems(IEnumerable<FormItemContract> formItems)
    {
        FormItems.Clear();
        foreach (var item in formItems)
        {
            FormItems.Add(item);
        }
    }
}
