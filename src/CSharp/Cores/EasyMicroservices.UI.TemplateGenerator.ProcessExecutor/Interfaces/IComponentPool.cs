namespace EasyMicroservices.UI.TemplateGenerator.Blazor.Interfaces;
public interface IComponentPool
{
    void SetParent(IParentComponent parent, IParentComponent child);
}
