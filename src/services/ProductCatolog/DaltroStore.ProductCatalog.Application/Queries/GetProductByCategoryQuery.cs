using DaltroStore.Core.Communication.Query;
using DaltroStore.ProductCatalog.Application.Queries.ViewModels;

namespace DaltroStore.ProductCatalog.Application.Queries
{
    public class GetProductByCategoryQuery : IQuery<ProductViewModel>
    {
        public Guid CategoryId { get; init; }
    }
}