using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProjectManagement.Entities;
using ProjectManagement.Shared;
using System;
using XUnitTestProject;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Create a new service provider.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Add a database context (ApplicationDbContext) usingle an in-memory 
            // database for testing.
            services.AddDbContext<PMContext<BaseEntity>>(context =>
            {
                context.UseInMemoryDatabase("ProjectManagment");
                context.UseInternalServiceProvider(serviceProvider);
            });
            services.AddDbContext<PMContext<Project>>(context =>
            {
                context.UseInMemoryDatabase("Project");
                context.UseInternalServiceProvider(serviceProvider);
            });
            services.AddDbContext<PMContext<Task>>(context =>
            {
                context.UseInMemoryDatabase("Task");
                context.UseInternalServiceProvider(serviceProvider);
            });
            services.AddDbContext<PMContext<User>>(context =>
            {
                context.UseInMemoryDatabase("User");
                context.UseInternalServiceProvider(serviceProvider);
            });

            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database
            // context (ApplicationDbContext).
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var taskDb = scopedServices.GetRequiredService<PMContext<Task>>();
                var projectDb = scopedServices.GetRequiredService<PMContext<Project>>();
                var userDb = scopedServices.GetRequiredService<PMContext<User>>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                // Ensure the database is created.
                taskDb.Database.EnsureCreated();
                projectDb.Database.EnsureCreated();
                userDb.Database.EnsureCreated();

                try
                {
                    // Seed the database with test data.
                    Utilities.InitializeDbForTests(taskDb, userDb, projectDb);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the database. Error: {Message}", ex.Message);
                }
            }
        });
    }
}