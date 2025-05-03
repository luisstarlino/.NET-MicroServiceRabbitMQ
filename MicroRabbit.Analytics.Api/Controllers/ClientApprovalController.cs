using MicroRabbit.Analytics.Application.Interfaces;
using MicroRabbit.Analytics.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Analytics.Api.Controllers
{
    [ApiController]
    public class ClientApprovalController : ControllerBase
    {
        private readonly IClientApprovalService _clientApprovalService; 

       public ClientApprovalController(IClientApprovalService clientApprovalService)
        {
            _clientApprovalService = clientApprovalService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClientApproval>> GetAllClientProcess()
        {
            return Ok(_clientApprovalService.GetAllClientApprovals());
        }

        [HttpGet]
        [Route("check-status")]
        public IActionResult GetApprovalByClientId([FromBody]string clientId)
        {
            try
            {
                //------------------------------------------------------------------------------------------------
                // CHECK PARMS
                //------------------------------------------------------------------------------------------------
                if(string.IsNullOrEmpty(clientId)) return BadRequest("ERRO02 - Send a valid clientId");
                var clientNumber = Int32.Parse(clientId);

                //------------------------------------------------------------------------------------------------
                // GET PROCESS
                //------------------------------------------------------------------------------------------------
                var clientApprovalProcess = _clientApprovalService.GetUniqueProcessByClient(clientNumber);
                if (clientApprovalProcess == null) return Ok("NO DATA");
                else
                {
                    return Ok(clientApprovalProcess);
                }

            }
            catch (Exception e)
            {
                return BadRequest("ERR01 - Internal Server Error");
            }
        }
    }
}
