using DaltroStore.Core.Communication.Query;
using DaltroStore.ProductCatalog.Application.Queries;
using DaltroStore.ProductCatalog.Application.Queries.ViewModels;
using DaltroStore.ProductCatalog.Domain.Models;
using DaltroStore.ProductCatalog.Infrastructure.Context;

namespace DaltroStore.ProductCatalog.Infrastructure.QueriesHandler
{
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductViewModel?>
    {
        private readonly ProductCatalogDbContext dbContext;

        public GetProductByIdQueryHandler(ProductCatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ProductViewModel?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product? product = await dbContext.Products.FindAsync(request.ProductId, cancellationToken);
            if (product == null) return null;

            Category? category = await dbContext.Categories.FindAsync(product.CategoryId, cancellationToken);
            return ProductMapper.ToViewModel(product, category);
        }
    }
}
