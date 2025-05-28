using MicroRabbit.Analytics.Data.Context;
using MicroRabbit.Analytics.Domain.EventHandlers;
using MicroRabbit.Analytics.Domain.Events;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Infra.IoC;
using MicroRabbit.Transfer.Domain.EventHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MicroRabbit.Analytics.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // --- Service Configuration
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddDbContext<AnalyticsDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("AnalyticsDbConnection")));
            services.AddDbContext<BankingDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("BankingDbConnection")));



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Analytics MicroService", Version = "v1" });
            });

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            RegisterServices(services);
        }

        // --- Rabbit COnfiguration 
        private void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services, Configuration);
        }

        // --- Pipeline Configuration 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Analytics Microservice v1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<ClientApprovalEvent, ClientApprovalHandler>();
        }

    }
}
