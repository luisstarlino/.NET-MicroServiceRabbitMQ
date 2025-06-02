using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace MicroRabbit.Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankingDbContext _ctx;

        public AccountRepository(BankingDbContext ctx)
        {
            _ctx = ctx;
        }

        async public Task<int> AddAccount(Account account)
        {
            try
            {
                await _ctx.Accounts.AddAsync(account);
                await _ctx.SaveChangesAsync();

                return account.Id;
            } catch
            {
                return -1;
            }
            
        }

        async public Task<bool> ChangeAccountStatus(int idAcc, bool newStatus)
        {
            try
            {
                var foundAcc = await _ctx.Accounts.FindAsync(idAcc);
                if (foundAcc is null) return false;

                // --- Update if has found!
                foundAcc.Active = newStatus;
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        async public Task<IEnumerable<Account>> GetAccounts()
        {
            return _ctx.Accounts;
        }

        async public Task<decimal?> UpdateAmountAcc(int idAcc, decimal incomeBalace)
        {
            try
            {
                var foundAcc = await _ctx.Accounts.FindAsync(idAcc);
                if (foundAcc is null) return null;
                else
                {
                    // --- New balance
                    foundAcc.AccountBalance = +(incomeBalace);
                    await _ctx.SaveChangesAsync();

                    return foundAcc.AccountBalance;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
