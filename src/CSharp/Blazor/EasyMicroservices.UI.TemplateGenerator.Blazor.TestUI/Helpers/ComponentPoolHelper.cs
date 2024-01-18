namespace EasyMicroservices.UI.TemplateGenerator.Blazor.TestUI.Helpers;
using EasyMicroservices.UI.TemplateGenerator.Blazor.Interfaces;

public class ComponentPoolHelper : IComponentPool
{
    public ComponentPoolHelper()
    {

    }

    public void SetParent<T>(IParentComponent parent, IParentComponent child, T model)
    {
        Console.WriteLine($"Parent: {parent.ToString()}");
        Console.WriteLine($"Child: {child.ToString()}");
        Console.WriteLine($"model: {model.ToString()}");
    }
}