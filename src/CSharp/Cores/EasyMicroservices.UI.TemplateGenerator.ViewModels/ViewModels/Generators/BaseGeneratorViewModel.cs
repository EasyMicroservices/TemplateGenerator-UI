using EasyMicroservices.UI.Cores;
using TemplateGenerators.GeneratedServices;

namespace EasyMicroservices.UI.TemplateGenerator.ViewModels.Generators;
public abstract class BaseGeneratorViewModel : BaseViewModel
{
    FormItemContract _FormItem;
    public FormItemContract FormItem
    {
        get
        {
            return _FormItem;
        }
        set
        {
            if (value.PrimaryFormItem != null)
                _FormItem = value.PrimaryFormItem;
            else
                _FormItem = value;
        }
    }
}
