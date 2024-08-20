using DaltroStore.Core.Data;
using DaltroStore.ProductCatalog.Domain.DomainServices;
using DaltroStore.Core.DomainObjects;
using Microsoft.Extensions.Logging;
using DaltroStore.Core.Communication.Command;

namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class DecreaseProductStockCommandHandler : ICommandHandler<DecreaseProductStockCommand, CommandResult>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger logger;
        private readonly StockService stockService;

        public DecreaseProductStockCommandHandler(IUnitOfWork unitOfWork,
                                                  StockService stockService,
                                                  ILoggerFactory loggerFactory)
        {
            this.unitOfWork = unitOfWork;
            this.stockService = stockService;
            this.logger = loggerFactory.CreateLogger(nameof(DecreaseProductStockCommandHandler));
        }

        public async Task<CommandResult> Handle(DecreaseProductStockCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await stockService.DecreaseStockQuantity(command.AggregateId, command.Quantity);
                await unitOfWork.Commit();
                return new CommandResult(CmdResultStatus.Success, error: string.Empty);
            }
            catch (EntityNotFoundException e)
            {
                logger.LogError(e, "product of Id: {Id} was not found", command.AggregateId);
                return new CommandResult(CmdResultStatus.AggregateNotFound, error: string.Empty);
            }
            catch(EntityValidationException e)
            {
                logger.LogError(e, e.Message);
                return new CommandResult(CmdResultStatus.InvalidDomainOperation, error: e.Message);
            }
        }
    }
}