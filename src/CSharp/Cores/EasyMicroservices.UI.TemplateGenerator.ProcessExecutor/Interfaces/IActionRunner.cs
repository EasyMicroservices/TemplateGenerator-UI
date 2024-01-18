using EasyMicroservices.ServiceContracts;

namespace EasyMicroservices.UI.TemplateGenerator.ProcessExecutor.Interfaces;
public interface IActionRunner
{
    Task<MessageContract> RunAsync(IComponent executor);
    ICollection<IActionRunner> Children { get; set; }
}
