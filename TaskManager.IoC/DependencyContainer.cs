using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Core.Commands;
using TaskManager.Core.Handlers.CommandHandlers;
using TaskManager.Core.Handlers.QueryHandlers;
using TaskManager.Core.Queries;
using TaskManager.Domain.DTOs;
using TaskManager.Repository.Implementations;
using TaskManager.Repository.Interfaces;
using TaskManager.Service.Implementations;
using TaskManager.Service.Interfaces;

namespace TaskManager.IoC
{
    public static class DependencyContainer
    {
        /// <summary>
        /// Registers services for TaskManager.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void RegisterServices(IServiceCollection services)
        {
            // Register services
            services.AddScoped<IUserTaskService, UserTaskService>();

            // Register repositories
            services.AddScoped<IUserTaskRepository, UserTaskRepository>();

            // Register Query Handlers
            services.AddTransient<IRequestHandler<GetAllUserTasksQuery, IEnumerable<UserTaskResponseDTO>>, GetAllUserTasksQueryHandler>();
            services.AddTransient<IRequestHandler<GetUserTaskByIdQuery, UserTaskResponseDTO>, GetUserTaskByIdQueryHandler>();

            // Register Command Handlers
            services.AddTransient<IRequestHandler<CreateUserTaskCommand, UserTaskResponseDTO>, CreateUserTaskCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateUserTaskCommand, UserTaskResponseDTO>, UpdateUserTaskCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteUserTaskCommand, bool>, DeleteUserTaskCommandHandler>();

        }
    }
}