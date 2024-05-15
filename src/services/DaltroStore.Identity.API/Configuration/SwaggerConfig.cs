using Microsoft.OpenApi.Models;

namespace DaltroStore.Identity.API.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection ConfigSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "DaltroStore Identity API"
                });
            });

            return services;
        }
    }
}
