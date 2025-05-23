using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Banking.Data.Context
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Client)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Cascade); // ou Restrict, se não quiser excluir em cascata
        }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
