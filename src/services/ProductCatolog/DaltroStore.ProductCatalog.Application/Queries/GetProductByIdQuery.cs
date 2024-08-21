using DaltroStore.Core.Communication.Query;
using DaltroStore.ProductCatalog.Application.Queries.ViewModels;

namespace DaltroStore.ProductCatalog.Application.Queries
{
    public class GetProductByIdQuery : IQuery<ProductViewModel>
    {
        public Guid ProductId { get; init; }
    }
}