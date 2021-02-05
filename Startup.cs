using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MovieApp.Context;
using MovieApp.Interface;
using MovieApp.Service;

namespace MovieApp
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration
        {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IDirectorRepository, DirectorRepository>();

            services.AddSwaggerDocument();
            //services.AddSwaggerGen(options => { options.SwaggerDoc("v1", new OpenApiInfo { Title = "Instrukcja", Description = "Laboratorium", Version = "v1.0" }); });

            services.AddDbContext<InstrukcjaDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("InstrukcjaConnection")) ;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapGet("/", async context =>
            //     {
            //         await context.Response.WriteAsync("Hello World!");
            //     });
            // });
            app.UseEndpoints(endpoints => { endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"); });
            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
