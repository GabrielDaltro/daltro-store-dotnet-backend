using DaltroStore.Core.Communication.Command;

namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class DecreaseProductStockCommand : Command<CommandResult>
    {
        public uint Quantity { get; init; }

        public DecreaseProductStockCommand(Guid aggregateId, uint quantity)
        {
            AggregateId = aggregateId;
            Quantity = quantity;
        }
    }
}