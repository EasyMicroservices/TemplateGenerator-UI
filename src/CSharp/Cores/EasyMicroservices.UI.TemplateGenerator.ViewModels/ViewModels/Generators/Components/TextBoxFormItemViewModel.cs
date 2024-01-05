using EasyMicroservices.UI.Cores;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Generators.Components;
public class TextBoxFormItemViewModel : BaseViewModel
{
    public TextBoxFormItemViewModel()
    {

    }

    public FormItemContract FormItem { get; set; }
    public string Value { get; set; }
}
