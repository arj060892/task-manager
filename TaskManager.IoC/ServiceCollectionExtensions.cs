using Microsoft.Extensions.DependencyInjection;
using TaskManager.Repository.Implementations;
using TaskManager.Repository.Interfaces;
using TaskManager.Service.Implementations;
using TaskManager.Service.Interfaces;

namespace TaskManager.IoC
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers services for TaskManager.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void RegisterTaskManagerServices(this IServiceCollection services)
        {
            // Register services
            services.AddScoped<IUserTaskService, UserTaskService>();

            // Register repositories
            services.AddScoped<IUserTaskRepository, UserTaskRepository>();

            // Any other necessary services or configurations can be added here
        }
    }
}
