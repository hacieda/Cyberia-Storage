using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddDbContext<StorageDbContext>(...);
        // services.AddScoped<IFileRepository, FileRepository>();
        // services.AddScoped<IFileStorage, LocalFileStorage>();

        return services;
    }
}

