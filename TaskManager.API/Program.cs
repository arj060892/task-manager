using System.Reflection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using TaskManager.Data;
using TaskManager.Domain.Mappings;
using TaskManager.IoC;

namespace TaskManager.API
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Task Manager", Version = "v1" });
            });

            builder.Services.AddDbContext<UserTaskManagerDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerDbConnection"));
            });

            DependencyContainer.RegisterServices(builder.Services);

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            builder.Services.AddAutoMapper(typeof(UserTaskProfile).GetTypeInfo().Assembly);


            // Initialize Serilog
            var logger = new LoggerConfiguration()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // File logging
                .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            // Global Exception Handling
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    if (exceptionHandlerPathFeature?.Error is Exception exception)
                    {
                        Log.Error(exception, "An unhandled exception occurred");
                    }
                    await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                });
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Task Manager V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            Log.CloseAndFlush();
        }
    }
}