using DaltroStore.Core.Communication;

namespace DaltroStore.ProductCatalog.Application.Commands
{
    public class RegisterProductCommandHandler : ICommandHandler<RegisterProductCommand, bool>
    {
        public Task<bool> Handle(RegisterProductCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}