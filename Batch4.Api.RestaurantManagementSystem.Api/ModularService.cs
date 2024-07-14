namespace Batch4.Api.RestaurantManagementSystem.Api;

public static class ModularService
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddDataAccessServices();
        services.AddBusinessLogicServices();
        return services;
    }

    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {        
        services.AddScoped<DA_MenuItem>();
        services.AddScoped<DA_Order>();
        services.AddScoped<DA_Category>();
        services.AddScoped<DA_Tax>();
        return services;
    }

    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {        
        services.AddScoped<BL_MenuItem>();
        services.AddScoped<BL_Order>();
        services.AddScoped<BL_Category>();
        services.AddScoped<BL_Tax>();        
        return services;
    }

}
