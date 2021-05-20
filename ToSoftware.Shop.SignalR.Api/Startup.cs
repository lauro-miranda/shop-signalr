using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using ToSoftware.Shop.SignalR.Api.Hubs;

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

            services.AddScoped<IUser, User>();
            services.Configure<NoSQLSettings>(Configuration.GetSection(nameof(NoSQLSettings)));

            services.AddHttpContextAccessor();

            services.AddCors();

            services.AddSignalR(op =>
            {
                op.EnableDetailedErrors = true;
                op.MaximumReceiveMessageSize = null;
            });

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHub<ShopHub>($"/{nameof(ShopHub)}");
            });
        }
    }
}