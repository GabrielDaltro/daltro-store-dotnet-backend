using DaltroStore.Core.Data;
using DaltroStore.Core.Communication;
using DaltroStore.ProductCatalog.Domain.Models;
using DaltroStore.ProductCatalog.Domain.Repositories;
using Microsoft.Extensions.Logging;
using DaltroStore.Core.DomainObjects;

namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class UnregisterProductCommandHandler : ICommandHandler<UnregisterProductCommand, CommandResult>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductRepository productRepository;
        private readonly ILogger logger;

        public UnregisterProductCommandHandler(IUnitOfWork unitOfWork,
                                               IProductRepository productRepository,
                                               ILoggerFactory loggerFactory)
        {
            this.unitOfWork = unitOfWork;
            this.productRepository = productRepository;
            this.logger = loggerFactory.CreateLogger<UnregisterProductCommandHandler>();
        }

        public async Task<CommandResult> Handle(UnregisterProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Product? product = await productRepository.GetById(command.ProductId);
                if (product == null)
                    return new CommandResult(CmdResultStatus.AggregateNotFound, error: string.Empty);

                productRepository.Delete(product);
                await unitOfWork.Commit(cancellationToken);
                return new CommandResult(CmdResultStatus.Success, error: string.Empty);
            }
            catch (DomainException e)
            {
                logger.LogError(e, "Error to delete product of Id {Id}.", command.ProductId);
                return new CommandResult(CmdResultStatus.InvalidDomainOperation, $"Error to delete product of Id {command.ProductId}. {e.Message}");
            }
        }
    }
}