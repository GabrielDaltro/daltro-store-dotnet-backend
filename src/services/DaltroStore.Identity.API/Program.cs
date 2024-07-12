using DaltroStore.Identity.API.Data;
using Microsoft.EntityFrameworkCore;
using DaltroStore.Identity.API.Configuration;
using DaltroStore.Identity.API.Services;

var builder = WebApplication.CreateBuilder(args);

var environmentName = builder.Environment.EnvironmentName;
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.AddScoped<JwtGeneratorService>();

builder.Services.AddDbContext<AppDbContext>(optionsBuilder => 
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.ConfigIdentity(builder.Configuration);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
        options.SuppressMapClientErrors = true;
    });

builder.Services.ConfigSwagger();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Development", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseExceptionHandler("/error-development");
else
    app.UseExceptionHandler("/error");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Development");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
