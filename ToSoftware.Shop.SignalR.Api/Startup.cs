using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToSoftware.Shop.SignalR.Api.Domain;
using ToSoftware.Shop.SignalR.Api.Domain.Settings;
using ToSoftware.Shop.SignalR.Api.Extensions;
using ToSoftware.Shop.SignalR.Api.Hubs;
using ToSoftware.Shop.SignalR.Api.Repositories;
using ToSoftware.Shop.SignalR.Api.Repositories.Contracts;

namespace ToSoftware.Shop.SignalR.Api
{
    public class Startup
    {
        IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<IUser, User>()
                .AddTransient<ICustomerRepository, CustomerRepository>();
            services.Configure<NoSQLSettings>(Configuration.GetSection(nameof(NoSQLSettings)));

            services.AddJWTAuthentication(Configuration.GetSection("JTWSettings:Secret").Value);

            services.AddHttpContextAccessor();

            services.AddCors();

            services
                .AddSignalR()
                .AddRedis(Configuration.GetSection("Redis:Connection").Value);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHub<ShopHub>("/ShopHub");
            });
        }
    }
}