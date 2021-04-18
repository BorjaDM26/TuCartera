using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using TuCartera.DBModel;

namespace TuCartera
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register SQL database configuration context as services
            string contextString = Configuration.GetConnectionString("TuCarteraContext");
            services.AddScoped<IAdapter>(_ => new Adapter(contextString));

            // Use Cross Origin Resource Sharing
            services.AddCors();

            // Register Automapper
            services.AddAutoMapper(typeof(Startup));

            // Register custom services
            services.AddHttpContextAccessor();
            services.AddScoped<Services.IUsersService, Services.UsersService>();

            services.AddControllers()
                    .AddJsonOptions(options => {
                        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie();

            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "ClientApp";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            //app.UseSpaStaticFiles();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseSpa(spa =>
            //{
            //    if (!env.IsDevelopment()) { 
            //        spa.Options.SourcePath = "ClientApp";
            //    }
            //});
        }
    }
}
