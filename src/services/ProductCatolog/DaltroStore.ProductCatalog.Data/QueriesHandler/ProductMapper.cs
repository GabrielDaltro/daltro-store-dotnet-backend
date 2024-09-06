using DaltroStore.ProductCatalog.Application.Queries.ViewModels;
using DaltroStore.ProductCatalog.Domain.Models;

namespace DaltroStore.ProductCatalog.Infrastructure.QueriesHandler
{
    public static class ProductMapper
    {
        public static ProductViewModel ToViewModel(Product product, Category? category)
        {
            return new ProductViewModel(
                    product.Id,
                    product.Name,
                    product.Price,
                    product.Description,
                    product.Image,
                    product.Weight,
                    product.Dimension.Width,
                    product.Dimension.Height,
                    product.Dimension.Depth,
                    category?.Name ?? string.Empty
                    );
        }
    }
}
