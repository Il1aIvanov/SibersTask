using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic;

public static class Extensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IProjectService, ProjectService>();
        serviceCollection.AddScoped<IEmployeeService, EmployeeService>();
        return serviceCollection;
    }
}