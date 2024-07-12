using DaltroStore.Identity.API.Data;
using DaltroStore.Identity.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DaltroStore.Identity.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection ConfigIdentity(this IServiceCollection services, IConfiguration configuration) 
        {
            services
            .AddDefaultIdentity<IdentityUser>(configureOptions =>
            {
                configureOptions.User.RequireUniqueEmail = true;
                configureOptions.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddErrorDescriber<IdentityPortugueseErrorDescriber>()
            .AddDefaultTokenProviders();

            IConfigurationSection appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            AppSettings appSettings = appSettingsSection.Get<AppSettings>() ?? throw new Exception("AppSettings is null");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = true;
                bearerOptions.SaveToken = true;
                bearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret)),

                    ValidateIssuer = true,
                    ValidIssuer = appSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = appSettings.Audience
                };
            });

            return services;
        }
    }
}
