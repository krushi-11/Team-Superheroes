using SuperHeroes.WebSite.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace SuperHeroes.WebSite
{
    /// <summary>
    /// Startup class for configuring  services and the HTTP request pipeline.
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure the services and adds them to the dependency injection container.
        /// </summary>
        /// <param name="services"></param> The service collection to configure.
        public void ConfigureServices(IServiceCollection services)
        {
            //configure razor pages with runtime compilation for development.
            services.AddRazorPages().AddRazorRuntimeCompilation();

            //Add server side blazor support
            services.AddServerSideBlazor();

            //Add HTTP client for making HTTP requests
            services.AddHttpClient();

            //Add MVC controllers.
            services.AddControllers();

            //Add a transient service for the JSonFileProductService.
            services.AddTransient<JsonFileProductService>();
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //use developer exception page in development mode.
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Redirect HTTP requests to HTTPS
            app.UseHttpsRedirection();

            //serve static files
            app.UseStaticFiles();

            //Enable routing
            app.UseRouting();

            //Enable authorization
            app.UseAuthorization();

            //Configure endpoints for routing
            app.UseEndpoints(endpoints =>
            {
                //Map Razor pages
                endpoints.MapRazorPages();

                //Map MVC Controllers
                endpoints.MapControllers();

                //Map Blazor hub
                endpoints.MapBlazorHub();

                // endpoints.MapGet("/products", (context) => 
                // {
                //     var products = app.ApplicationServices.GetService<JsonFileProductService>().GetProducts();
                //     var json = JsonSerializer.Serialize<IEnumerable<Product>>(products);
                //     return context.Response.WriteAsync(json);
                // });
            });

            // Custom 404 error handling middleware
            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
                {
                    // Handle 404 errors here
                    context.Response.Redirect("/PageNotFound");
                }
            });
        }
    }
}