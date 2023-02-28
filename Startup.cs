using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project.Data;
using Microsoft.EntityFrameworkCore;
using Project.Repositories;

namespace Projects
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProjectRepository, ProjectRepository>();

            services.AddDbContext<ProjectContext>(options =>
                options.UseSqlite("Data Source=Data/Project.db"));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ProjectContext projectContext,IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>  // localhost:####/?secret
            {
                if (context.Request.Query.ContainsKey("secret"))
                {
                    context.Response.Headers.Add("content-type", "text/html");
                    await context.Response.WriteAsync("You found the easter egg!");
                }
                else
                {
                    await next.Invoke();
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // delete/create database
            projectContext.Database.EnsureDeleted();  // only for testing purposes
            projectContext.Database.EnsureCreated();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{projectNum?}");
            });

            app.Run(async (context) =>  // localhost:####/id
            {
                context.Response.Headers.Add("content-type", "text/html");
                await context.Response.WriteAsync(
                    "ERROR: Your request did not match any configured routing!");
            });
        }
    }
}
