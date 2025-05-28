using MicroRabbit.Analytics.Data.Context;
using MicroRabbit.Analytics.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Analytics.Data.Repository
{
    public class BankingRepository : IBankingRepository
    {
        //        private BankingDbContext _ctx;
        private BankingDbContext _ctx;

        public BankingRepository(BankingDbContext ctx)
        {
            _ctx = ctx;
        }


        async public Task<bool> UpdateClientStatus(int idClient, bool status)
        {
            try
            {
                // --- Found client
                var foundClient = await _ctx.Clients.FindAsync(idClient);
                if (foundClient is null) return false;
                else
                {
                    foundClient.IsActive = status;
                    await _ctx.SaveChangesAsync();
                    return true;
                }

            } catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
