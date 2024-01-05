using EasyMicroservices.UI.Cores;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Generators;
public class FormItemSingleItemGeneratorViewModel : BaseViewModel
{
    public FormItemSingleItemGeneratorViewModel()
    {

    }

    public FormItemContract FormItem { get; set; }

    public async Task InitializeFormItem(FormItemContract formItem)
    {

    }
}
