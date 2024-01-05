using EasyMicroservices.UI.Cores;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Generators.Components;
public class DateOnlyFormItemViewModel : BaseGeneratorViewModel
{
    public DateOnlyFormItemViewModel()
    {

    }

    public DateTime? SelectedValue { get; set; }
}
