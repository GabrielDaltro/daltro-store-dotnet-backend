using DaltroStore.Core.Communication.Query;
using DaltroStore.ProductCatalog.Application.Queries.ViewModels;

namespace DaltroStore.ProductCatalog.Application.Queries
{
    internal class GetProductByIdQuery : IQuery<ProductViewModel>
    {
        public Guid ProductId { get; init; }
    }
}