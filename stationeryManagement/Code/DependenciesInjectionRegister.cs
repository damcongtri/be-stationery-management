using Microsoft.EntityFrameworkCore;
using stationeryManagement.Data;
using stationeryManagement.Data.Common.DbContext;
using stationeryManagement.Service;
using stationeryManagement.Service.Interface;

namespace stationeryManagement.Code;

public static class DependenciesInjectionRegister
{
    public static void RegisterDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationContext>(options => options
            .UseSqlServer(builder.Configuration.GetConnectionString("ApplicationContextConnection") ?? throw new InvalidOperationException("Connection string 'DatabaseContextConnection' not found."), b => b.MigrationsAssembly("stationeryManagement"))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        );
        
        builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
        builder.Services.AddScoped<IDbContext, ApplicationContext>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<ISupplierService, SupplierService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<IStationeryService, StationeryService>();
    }
}