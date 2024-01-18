namespace EasyMicroservices.UI.TemplateGenerator.ProcessExecutor.Interfaces;
public interface IComponent
{
    string LocalVariableName { get; }
    object ControlInstance { get; set; }
    object ValueInstance { get; set; }
    IComponent Parent { get; set; }
    ICollection<IComponent> Children { get; }
    void AddChild(IComponent component);
    void AddChildren(IEnumerable<IComponent> components);
    void RemoveChild(IComponent component);
}
