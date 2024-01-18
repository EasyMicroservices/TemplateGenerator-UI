using EasyMicroservices.UI.TemplateGenerator.Blazor.Interfaces;

namespace EasyMicroservices.UI.TemplateGenerator.Blazor.TestUI.Helpers;

public class ParentComponentHelper : IParentComponent
{
    public IParentComponent Parent { get; set; }
}