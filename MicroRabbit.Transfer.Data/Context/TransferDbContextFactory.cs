using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;


namespace MicroRabbit.Transfer.Data.Context
{
    public class TransferDbContextFactory : IDesignTimeDbContextFactory<TransferDbContext>
    {
        public TransferDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TransferDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bakingdb;Username=admin;Password=admin123");

            return new TransferDbContext(optionsBuilder.Options);
        }
    }
}
