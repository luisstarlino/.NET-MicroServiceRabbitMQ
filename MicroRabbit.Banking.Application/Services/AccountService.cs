using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Domain.Core.Bus;

namespace MicroRabbit.Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventBus _bus;

        public AccountService(IAccountRepository accountRepository, IEventBus bus)
        {
            _accountRepository = accountRepository;
            _bus = bus;
        }

        async public Task<AccountResponse> AddAccount(AccountRequest acRequest)
        {
            var response = new AccountResponse();
            try
            {
                var dbModel = new Account
                {
                    AccountBalance = acRequest.AccountBalance,
                    ClientId = acRequest.ClientId,
                    AccountType = (AccountType)acRequest.AccountType
                };
                var newAcc = await _accountRepository.AddAccount(dbModel);

                if (newAcc < 0) throw new Exception("-1");
                
                //--------------------------------------------------
                // --- All ready done! Account created!
                //--------------------------------------------------
                response.IdAccount = newAcc;

                //--------------------------------------------------
                // --- Add event to send email (** To do **)
                //--------------------------------------------------

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErroMessage = $"Erro interno. Entre em contato com o administrador | {ex.Message}";
            }

            return response;
            
        }

        async public Task<IEnumerable<Account>> GetAccounts()
        {
            return await _accountRepository.GetAccounts();
        }

        public bool Transfer(AccountTransfer accountTransfer)
        {
            // --- Creating a handler command to transfer money
            var createTransferCommand = new CreateTransferCommand(accountTransfer.FromAccount,accountTransfer.ToAccount, accountTransfer.TransferAmount);

            // --- Send command using core
            _bus.SendCommand(createTransferCommand);

            return true;
        }

        async public Task<bool> UpdateStatusAccout(int accId, bool newStatus)
        {
            //--------------------------------------------------
            // --- UPDATE STATUS
            //--------------------------------------------------
            var updateSuccess = await _accountRepository.ChangeAccountStatus(accId, newStatus);
            return updateSuccess;
        }
    }
}
