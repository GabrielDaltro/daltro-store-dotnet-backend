using DaltroStore.Core.Communication.Command;

namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class UnregisterProductCommand : Command<CommandResult>
    {
        public Guid ProductId { get; init; }

        public UnregisterProductCommand(Guid productId)
        {
            ProductId = productId;
        }
    }
}
