namespace EasyMicroservices.UI.TemplateGenerator.Blazor.Interfaces;
public interface IParentComponent
{
    public object ValueInstance { get; }
    public IParentComponent Parent { get; set; }
}
