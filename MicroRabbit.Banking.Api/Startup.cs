
using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Infra.IoC;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Banking.Api
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
            services.AddSwaggerGen();

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
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

}
