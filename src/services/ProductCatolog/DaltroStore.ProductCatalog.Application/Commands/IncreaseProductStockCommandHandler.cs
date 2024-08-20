using DaltroStore.Core.Data;
using DaltroStore.ProductCatalog.Domain.DomainServices;
using DaltroStore.Core.DomainObjects;
using Microsoft.Extensions.Logging;
using DaltroStore.Core.Communication.Command;

namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class IncreaseProductStockCommandHandler : ICommandHandler<IncreaseProductStockCommand, CommandResult>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly StockService stockService;
        private readonly ILogger logger;

        public IncreaseProductStockCommandHandler(IUnitOfWork unitOfWork, 
                                                  StockService stockService,
                                                  ILoggerFactory loggerFactory)
        {
            this.unitOfWork = unitOfWork;
            this.stockService = stockService;
            this.logger = loggerFactory.CreateLogger<IncreaseProductStockCommandHandler>();
        }

        public async Task<CommandResult> Handle(IncreaseProductStockCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await stockService.IncreaseStockQuantity(command.AggregateId, command.Quantity);
                await unitOfWork.Commit();
                return new CommandResult(CmdResultStatus.Success, error: string.Empty);
            }
            catch (EntityNotFoundException e)
            {
                logger.LogError(e, "product of Id: {Id} was not found", command.AggregateId);
                return new CommandResult(CmdResultStatus.AggregateNotFound, error: $"product of Id: {command.AggregateId} was not found");
            }
        }
    }
}