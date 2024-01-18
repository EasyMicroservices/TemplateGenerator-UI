namespace EasyMicroservices.UI.TemplateGenerator.Blazor.Interfaces;
public interface IComponentPool
{
    void SetParent<T>(IParentComponent parent, IParentComponent child, T model);
}
