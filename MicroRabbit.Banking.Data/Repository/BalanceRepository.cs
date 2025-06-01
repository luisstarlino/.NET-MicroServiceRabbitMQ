using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Data.Repository
{
    public class BalanceRepository : IBalanceRepository
    {
        private BankingDbContext _ctx;

        public BalanceRepository(BankingDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Guid> AddBalance(Balance balance)
        {
            try
            {
                await _ctx.Balances.AddAsync(balance);
                await _ctx.SaveChangesAsync();

                return balance.Id;
            } catch (Exception ex)
            {
                return Guid.Empty;
            }
        }

        async public Task<IEnumerable<Balance>> GetAllBalances()
        {
            return _ctx.Balances;
        }

        public async Task<IEnumerable<Balance>> GetAllBalancesByAccount(int AccountId)
        {
            var byAccount = _ctx.Balances.Select((b)=> b.AccountId.Equals(AccountId));
            return (IEnumerable<Balance>)byAccount;
        }

        public async Task<Balance?> GetUniqueBalance(Guid guidBalance)
        {
            var foundBalance = await _ctx.Balances.FindAsync(guidBalance);
            if (foundBalance is null) return null;
            else
            {
                return foundBalance;
            }
        }
    }
}
