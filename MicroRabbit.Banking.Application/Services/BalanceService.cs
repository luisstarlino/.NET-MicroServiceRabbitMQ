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
        private readonly IAccountRepository _accountRepository;

        public BalanceService(IBalanceRepository balanceRepository, IAccountRepository accountRepository)
        {
            _balanceRepository = balanceRepository;
            _accountRepository = accountRepository;
        }
        public async Task<Guid> AddBalance(BalanceRequest balance, int accountId)
        {
            try
            {
                // ------------------------------------------------------------------------------------------------
                // 1. Updating the account according the incoming balance
                //------------------------------------------------------------------------------------------------
                var newBalanceOperator = await _accountRepository.UpdateAmountAcc(accountId, balance.Amount);
                if (newBalanceOperator is null) return Guid.Empty;

                // ------------------------------------------------------------------------------------------------
                // 2. Add a balance history
                //------------------------------------------------------------------------------------------------
                var dbModel = new Balance
                {
                    AccountId = accountId,
                    Amount = balance.Amount,
                    Description = balance.Description
                };

                var saveBalanceHistory = await _balanceRepository.AddBalance(dbModel);
                return saveBalanceHistory;


            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
            
        }

        public async Task<IEnumerable<Balance?>> ListAllBalancesByAcc(int accountId)
        {
            var repositoryLayer = await _balanceRepository.GetAllBalancesByAccount(accountId);
            return repositoryLayer;
        }
    }
}
