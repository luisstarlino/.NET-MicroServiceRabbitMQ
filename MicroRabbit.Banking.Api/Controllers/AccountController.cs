using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Banking.Api.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddAccount([FromBody] AccountRequest acRequest)
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
                var isAccountCreated = true;

                //------------------------------------------------------------------------------------------------
                // CHECK IF IS OK
                //------------------------------------------------------------------------------------------------


            }
            catch (Exception ex)
            {
                return BadRequest("ERR01-Error trying to create a new account into the Bank");
            }
        }
    }
}
