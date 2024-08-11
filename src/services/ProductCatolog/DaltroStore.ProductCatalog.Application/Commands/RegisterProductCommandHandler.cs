using DaltroStore.Core.Data;
using DaltroStore.Core.Communication;
using DaltroStore.ProductCatalog.Domain.Models;
using DaltroStore.ProductCatalog.Domain.Repositories;
using DaltroStore.Core.DomainObjects;

namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class RegisterProductCommandHandler : ICommandHandler<RegisterProductCommand, CommandResult>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductRepository productRepository;

        public RegisterProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            this.unitOfWork = unitOfWork;
            this.productRepository = productRepository;
        }

        public async Task<CommandResult> Handle(RegisterProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var product = Map(command);
                productRepository.Add(product);
                await unitOfWork.Commit(cancellationToken);
                return new CommandResult(CmdResultStatus.Success, error: string.Empty);
            }
            catch (DomainException e)
            {
                return new CommandResult(CmdResultStatus.InvalidDomainOperation, error: e.Message);
            }
        }

        private static Product Map(RegisterProductCommand command)
        {
            return new Product(
                command.Name,
                command.Price,
                command.Description,
                command.Active,
                command.Image,
                command.CategoryId,
                command.RegistrationDate,
                command.Weight,
                new Dimension(command.Width, command.Height, command.Depth)
                );
        }
    }
}