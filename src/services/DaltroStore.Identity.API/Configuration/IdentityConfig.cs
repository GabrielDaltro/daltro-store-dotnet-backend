using Microsoft.AspNetCore.Identity;
using DaltroStore.Identity.API.Data;
using DaltroStore.Auth.Jwt;

namespace DaltroStore.Identity.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection ConfigIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services
            .AddIdentity<IdentityUser, IdentityRole>(configureOptions =>
            {
                configureOptions.User.RequireUniqueEmail = true;
                configureOptions.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddErrorDescriber<IdentityPortugueseErrorDescriber>()
            .AddDefaultTokenProviders();

            services.ConfigureJwt(configuration);

            return services;
        }
    }
}