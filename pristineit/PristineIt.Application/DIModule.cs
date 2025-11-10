using Microsoft.Extensions.DependencyInjection;
using PristineIt.Application.Services;
using PristineIt.Domain.Tasks.Services;

namespace PristineIt.Application;

public static class DIModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITaskService, TaskService>();
        return services;
    }
}
