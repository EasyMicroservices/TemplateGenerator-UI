namespace EasyMicroservices.UI.TemplateGenerator.ProcessExecutor.Interfaces;
public interface IComponent
{
    string LocalVariableName { get; }
    IComponent Parent { get; set; }
    ICollection<IComponent> Children { get; }
    void AddChild(IComponent component);
}
