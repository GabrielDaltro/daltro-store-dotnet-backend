using DaltroStore.Core.Communication.Query;
using DaltroStore.ProductCatalog.Application.Queries.ViewModels;

namespace DaltroStore.ProductCatalog.Application.Queries
{
    public class GetAllProductsQuery : IQuery<IEnumerable<ProductViewModel>>
    {
    }
}
