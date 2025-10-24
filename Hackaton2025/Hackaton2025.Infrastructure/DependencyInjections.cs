using Hackaton2025.Domain.Models.Abstractions;
using Hackaton2025.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hackaton2025.Infrastructure;
public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IUniversityService, UniversityService>();
        services.AddScoped<IUniversityFacultyService, UniversityFacultyService>();
        services.AddScoped<IPastProcedureService, PastProcedureService>();
        services.AddScoped<IPastProcedureJuryMemberService, PastProcedureJuryMemberService>();

        return services;
    }
}
