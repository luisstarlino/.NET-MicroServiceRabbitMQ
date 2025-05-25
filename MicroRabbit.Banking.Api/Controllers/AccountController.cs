using MicroRabbit.Banking.Application.Models;
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
