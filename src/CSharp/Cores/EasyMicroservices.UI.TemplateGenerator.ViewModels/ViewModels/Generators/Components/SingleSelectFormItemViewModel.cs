using EasyMicroservices.UI.Cores;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Generators.Components;
public class SingleSelectFormItemViewModel : BaseViewModel
{
    public SingleSelectFormItemViewModel()
    {

    }

    public FormItemContract FormItem { get; set; }
    public FormItemContract SelectedValue { get; set; }
}
