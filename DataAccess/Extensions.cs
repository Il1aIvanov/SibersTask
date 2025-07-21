using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class Extensions
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection servicesCollection, 
        string connectionString)
    {
        servicesCollection.AddScoped<IProjectRepository, ProjectRepository>();
        servicesCollection.AddScoped<IEmployeeRepository, EmployeeRepository>();
        servicesCollection.AddDbContext<SibersDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        return servicesCollection;
    }
}