using EasyMicroservices.UI.Cores;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Generators.Components;
public class SingleSelectFormItemViewModel : BaseGeneratorViewModel
{
    public SingleSelectFormItemViewModel()
    {

    }

    public FormItemContract SelectedValue { get; set; }
}
