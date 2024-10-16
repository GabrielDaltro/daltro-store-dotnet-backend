using MediatR;
using DaltroStore.Core.Data;
using DaltroStore.Core.Communication.Command;
using DaltroStore.Core.Communication.Query;
using DaltroStore.Core.Communication.DomainEvent;
using DaltroStore.ProductCatalog.Domain;
using DaltroStore.ProductCatalog.Domain.DomainServices;
using DaltroStore.ProductCatalog.Domain.Repositories;
using DaltroStore.ProductCatalog.Application.Commands;
using DaltroStore.ProductCatalog.Application.Queries.ViewModels;
using DaltroStore.ProductCatalog.Application.Queries;
using DaltroStore.ProductCatalog.Application.DomainEventHandlers;
using DaltroStore.ProductCatalog.Infrastructure.QueriesHandler;
using DaltroStore.ProductCatalog.Infrastructure.Context;
using DaltroStore.ProductCatalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DaltroStore.ProductCatolog.API
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Communication
            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<IEventBus, EventBus>();

            // Persistence
            services.AddDbContext<ProductCatalogDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }, 
            ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWork>(services =>
            {
                return services.GetRequiredService<ProductCatalogDbContext>();
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            //Domain services
            services.AddScoped<StockService>();

            //Domain Event Handlers
            services.AddScoped<IRequestHandler<LowStockProductEvent>, LowStockProductEventHandler>();

            // Commands and Queries handlers
            services.AddScoped<IRequestHandler<RegisterProductCommand, CommandResult<Guid>>, RegisterProductCommandHandler>();
            services.AddScoped<IRequestHandler<UnregisterProductCommand, CommandResult>, UnregisterProductCommandHandler>();
            services.AddScoped<IRequestHandler<EditProductCommand, CommandResult>, EditProductCommandHandler>();
            services.AddScoped<IRequestHandler<IncreaseProductStockCommand, CommandResult>, IncreaseProductStockCommandHandler>();
            services.AddScoped<IRequestHandler<DecreaseProductStockCommand, CommandResult>, DecreaseProductStockCommandHandler>();
            services.AddScoped<IRequestHandler<GetProductByIdQuery, ProductViewModel?>, GetProductByIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllProductsQuery,IEnumerable<ProductViewModel>>, GetAllProductsQueryHandler>();
            
            return services;
        }
    }
}
