using DaltroStore.Core.Communication.Command;
using Microsoft.EntityFrameworkCore;
using DaltroStore.Core.Communication.DomainEvent;
using DaltroStore.Core.Communication.Query;
using DaltroStore.Core.Data;
using DaltroStore.Customers.Infrastructure;
using MediatR;

namespace DaltroStore.Customer.API
{
    internal static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            // Communication
            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<IEventBus, EventBus>();

            // Persistence
            services.AddDbContext<CustumerDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            },
            ServiceLifetime.Scoped);

            services.AddScoped<IUnitOfWork>(services =>
            {
                return services.GetRequiredService<CustumerDbContext>();
            });

            return services;
        }
    }
}
