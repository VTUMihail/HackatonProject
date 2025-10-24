using Hackaton2025.Domain.Models.Abstractions;
using Hackaton2025.Infrastructure.Abstractions;
using Hackaton2025.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hackaton2025.Infrastructure;
public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        

        return services;
    }
}
