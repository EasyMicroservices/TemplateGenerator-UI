using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Generators.Components;
public class MultipleSelectFormItemViewModel : BaseGeneratorViewModel
{
    public MultipleSelectFormItemViewModel()
    {

    }

    public IEnumerable<FormItemContract> SelectedValues { get; set; } = new HashSet<FormItemContract>();
}