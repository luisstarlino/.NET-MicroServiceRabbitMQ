using MicroRabbit.Analytics.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Analytics.Api.Controllers
{
    [ApiController]
    public class ClientApprovalController : ControllerBase
    {
        //private readonly

        [HttpGet]
        public ActionResult<IEnumerable<ClientApproval>> GetAllClientProcess()
        {
            return Ok();
        }
    }
}
