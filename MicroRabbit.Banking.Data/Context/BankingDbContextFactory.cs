using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Data.Context
{
    public class BankingDbContextFactory : IDesignTimeDbContextFactory<BankingDbContext>
    {
        public BankingDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BankingDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bakingdb;Username=admin;Password=admin123");

            return new BankingDbContext(optionsBuilder.Options);
        }
    }
}
