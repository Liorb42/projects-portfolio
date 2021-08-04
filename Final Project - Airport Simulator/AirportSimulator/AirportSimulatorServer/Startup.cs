using AirportSimulatorServer.Api.Hubs;
using AirportSimulatorServer.Data;
using AirportSimulatorServer.Services;
using AirportSimulatorShared.Interfaces;
using FlightsGenerator.BL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;


namespace AirportSimulatorServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                });
            });
            services.AddDbContext<AirportStateContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddSingleton<IFlightGenerator, FlightsGenerator1>();
            services.AddTransient<IAirportRepository, AirportRepository>();
            services.AddSingleton<IAirportBuilderService>(sp => new AirportBuilderService(sp.GetRequiredService<IFlightGenerator>()));
            services.AddSingleton<IAirportService, AirportService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AirportStateContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AirportHub>("/api/airport");

            });
        }
    }
}
