using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProjectManagement.Data.Interfaces;
using ProjectManagement.Data.Implementation;
using ProjectManagement.Entities;
using ProjectManagement.Api.Controllers;
using ProjectManagement.Shared;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Api
{
    public class Startup
    {
        public BaseEntity T;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IBaseRepository<BaseEntity>, BaseRepository<BaseEntity>>();
            services.AddDbContext<PMContext<BaseEntity>>(context =>
            {
                context.UseInMemoryDatabase("ProjectManagment");
            });
            services.AddDbContext<PMContext<Project>>(context =>
            {
                context.UseInMemoryDatabase("Project");
            });
            services.AddDbContext<PMContext<Task>>(context =>
            {
                context.UseInMemoryDatabase("Task");
            });
            services.AddDbContext<PMContext<User>>(context =>
            {
                context.UseInMemoryDatabase("User");
            });
            services.AddTransient<IBaseRepository<Task>, BaseRepository<Task>>();
            services.AddTransient<IBaseRepository<Project>, BaseRepository<Project>>();
            services.AddTransient<IBaseRepository<User>, BaseRepository<User>>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Project Management API",
                    Version = "v1",
                    Description = "Project Management API"
                });
            });
            //services.AddTransient<BaseController<T>, ProjectController>();
            //services.AddTransient<BaseController<Task>, TaskController>();
            //services.AddTransient<BaseController<User>, UserController>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project Management API V1");

            });
        }
    }
}
