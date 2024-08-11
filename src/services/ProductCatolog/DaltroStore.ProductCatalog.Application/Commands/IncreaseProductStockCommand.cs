using DaltroStore.Core.Messages;

namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class IncreaseProductStockCommand : Command<CommandResult>
    {
        public uint Quantity { get; init; }

        public IncreaseProductStockCommand(uint quantity)
        {
            Quantity = quantity;
        }
    }
}