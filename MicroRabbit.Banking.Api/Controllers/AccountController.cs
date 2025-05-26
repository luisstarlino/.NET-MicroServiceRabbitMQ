using MicroRabbit.Banking.Api.Controllers.Core;
using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Api.Controllers
{
    [ApiController]
    [Route("accounts")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAllAccounts()
        {
            try
            {
                //------------------------------------------------------------------------------------------------
                // LIST ACCOUNT'S
                //------------------------------------------------------------------------------------------------
                var accounts = await _accountService.GetAccounts();
                return CreateBaseResponse(System.Net.HttpStatusCode.OK, accounts);

            }
            catch (Exception ex)
            {
                return CreateBaseResponse(System.Net.HttpStatusCode.InternalServerError, "ERR01-Please, contact the administrator");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] AccountRequest acRequest)
        {
            try
            {
                // ------------------------------------------------------------------------------------------------
                // CHECK PARMS
                //------------------------------------------------------------------------------------------------
                if (!ModelState.IsValid) return BadRequest("Fill all the required parameters!");

                // ------------------------------------------------------------------------------------------------
                // CHECK IF THIS REQUEST HAS A ACCOUNT TYPE VALID
                //------------------------------------------------------------------------------------------------
                if(Enum.IsDefined(typeof(AccountType), acRequest.AccountType) == false)
                {
                    throw new Exception("Invalid Account type");
                }

                //------------------------------------------------------------------------------------------------
                // CREATING ACCOUNT
                //------------------------------------------------------------------------------------------------
                var isAccountCreated = await _accountService.AddAccount(acRequest);

                //------------------------------------------------------------------------------------------------
                // CHECK IF IS OK
                //------------------------------------------------------------------------------------------------
                if (isAccountCreated.Success)
                {
                    return CreateBaseResponse(System.Net.HttpStatusCode.Created, new
                    {
                        isAccountCreated.IdAccount,
                        Message = "Your new account has created. You will receive soon as possible a confirmation email type."
                    });
                }
                else throw new Exception(isAccountCreated.ErroMessage);

            }
            catch (Exception ex)
            {
                return CreateBaseResponse(System.Net.HttpStatusCode.InternalServerError, "ERR01-Error trying to create a new account into the Bank");
            }
        }
    }
}
