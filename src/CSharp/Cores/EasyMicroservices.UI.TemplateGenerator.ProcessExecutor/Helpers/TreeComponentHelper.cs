using EasyMicroservices.UI.TemplateGenerator.ProcessExecutor.Interfaces;

namespace EasyMicroservices.UI.TemplateGenerator.ProcessExecutor.Helpers;

public class TreeComponentHelper
{
    public IComponent Root { get; set; }

    public ICollection<IComponent> GetAllComponents(IComponent component)
    {
        var root = GetRoot(component);
        var items = GetAllChildren(root)
            .Distinct()
            .ToList();
        items.Add(root);
        return items;
    }

    public IComponent FindByInstance(object instance)
    {
        if (Root == null)
            return null;
        return GetAllComponents(Root).FirstOrDefault(x => x.ControlInstance == instance);
    }

    public IComponent FindByValueInstance(object instance)
    {
        if (Root == null)
            return null;
        return GetAllComponents(Root).FirstOrDefault(x => x.ValueInstance == instance);
    }

    public IComponent GetRoot(IComponent component)
    {
        IComponent current = component;
        while (current.Parent != null)
        {
            current = current.Parent;
        }
        return current;
    }

    public IEnumerable<IComponent> GetAllChildren(IComponent component)
    {
        foreach (var item in component.Children)
        {
            yield return item;
        }
    }
}
