using DaltroStore.Core.Communication.Query;

namespace DaltroStore.ProductCatalog.Application.Queries
{
    internal class GetProductByCategoryQuery : IQuery<ProductViewModel>
    {
        public Guid CategoryId { get; init; }
    }
}