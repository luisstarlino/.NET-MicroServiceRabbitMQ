using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Application.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepository _balanceRepository;

        public BalanceService(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }
        public async Task<Guid> AddBalance(BalanceRequest balance, int accountId)
        {
            try
            {
                // ------------------------------------------------------------------------------------------------
                // 1. Updating the account according the incoming balance
                //------------------------------------------------------------------------------------------------


                // ------------------------------------------------------------------------------------------------
                // 2. Add a balance history
                //------------------------------------------------------------------------------------------------
                var dbModel = new Balance
                {
                    AccountId = accountId,
                    Amount = balance.Amount,
                    Description = balance.Description
                };

            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
            
        }
    }
}
