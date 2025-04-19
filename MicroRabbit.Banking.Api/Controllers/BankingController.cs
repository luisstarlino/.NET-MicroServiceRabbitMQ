using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Banking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankingController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public BankingController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAllAccounts()
        {
            return Ok(_accountService.GetAccounts());
        }

        [HttpPost]
        public IActionResult PostTransfer([FromBody] AccountTransfer accountTransfer)
        {
            _accountService.Transfer(accountTransfer);
            return Ok(accountTransfer);
        }

    }
}
