namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Generators.Components;
public class DateTimeFormItemViewModel : BaseGeneratorViewModel
{
    public DateTimeFormItemViewModel()
    {

    }

    public DateTime? SelectedDateValue { get; set; }
    public TimeSpan? SelectedTimeValue { get; set; }
}
