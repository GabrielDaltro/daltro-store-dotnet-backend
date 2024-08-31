using DaltroStore.Core.Data;
using DaltroStore.ProductCatalog.Domain.Models;
using DaltroStore.ProductCatalog.Domain.Repositories;
using DaltroStore.Core.DomainObjects;
using DaltroStore.Core.Communication.Command;

namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class RegisterProductCommandHandler : ICommandHandler<RegisterProductCommand, CommandResult<Guid>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductRepository productRepository;

        public RegisterProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            this.unitOfWork = unitOfWork;
            this.productRepository = productRepository;
        }

        public async Task<CommandResult<Guid>> Handle(RegisterProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var product = Map(command);
                productRepository.Add(product);
                await unitOfWork.Commit(cancellationToken);
                return new CommandResult<Guid>(CmdResultStatus.Success, product.Id, error: string.Empty);
            }
            catch (DomainException e)
            {
                return new CommandResult<Guid>(CmdResultStatus.InvalidDomainOperation, Guid.Empty, error: e.Message);
            }
        }

        private static Product Map(RegisterProductCommand command)
        {
            return new Product(
                id: Guid.NewGuid(),
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