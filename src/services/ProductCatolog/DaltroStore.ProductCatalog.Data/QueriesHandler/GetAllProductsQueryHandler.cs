using DaltroStore.Core.Communication.Query;
using DaltroStore.ProductCatalog.Application.Queries;
using DaltroStore.ProductCatalog.Application.Queries.ViewModels;
using DaltroStore.ProductCatalog.Domain.Models;
using DaltroStore.ProductCatalog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DaltroStore.ProductCatalog.Infrastructure.QueriesHandler
{
    public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, IEnumerable<ProductViewModel>>
    {
        private readonly ProductCatalogDbContext dbContext;

        public GetAllProductsQueryHandler(ProductCatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductViewModel>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var productViewModels = new List<ProductViewModel>();
            IEnumerable<Product> products = await dbContext.Products.ToListAsync(cancellationToken);
            foreach (var product in products)
            {
                Category? category = await dbContext.Categories.FindAsync(product.CategoryId, cancellationToken);
                productViewModels.Add(ProductMapper.ToViewModel(product, category));
            }
            return productViewModels;
        }
    }
}
