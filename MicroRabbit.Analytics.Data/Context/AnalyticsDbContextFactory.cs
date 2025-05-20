using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Analytics.Data.Context
{
    public class AnalyticsDbContextFactory : IDesignTimeDbContextFactory<AnalyticsDbContext>
    {
        public AnalyticsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AnalyticsDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=analytics;Username=admin;Password=admin123");

            return new AnalyticsDbContext(optionsBuilder.Options);
        }
    }
}
