using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Data.Repository;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Banking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// ADD NEW CLIENT
        /// </summary>
        /// <param name="clientReq"></param>
        /// <returns></returns>
        [HttpPost]
        async public Task<IActionResult> AddClient([FromBody] ClientRequest clientReq)
        {
            try
            {
                //------------------------------------------------------------------------------------------------
                // CHECK PARMS
                //------------------------------------------------------------------------------------------------
                if (!ModelState.IsValid) return BadRequest("Fill all the required parameters!");

                //------------------------------------------------------------------------------------------------
                // TRY TO ADD
                //------------------------------------------------------------------------------------------------
                var isAccepted = await _clientService.AddClient(clientReq);

                //------------------------------------------------------------------------------------------------
                // CHECK IF IS OK
                //------------------------------------------------------------------------------------------------
                if (isAccepted) return Ok("Your account request has been sent for analysis. You will receive an email confirming the process shortly!");
                else
                {
                    return BadRequest("We were unable to complete your account request. Please, try again later!");
                }
            }
            catch
            {
                return BadRequest("ERR01-Error trying to create a new account into the Bank");
            }
        }

        /// <summary>
        /// List all Clients
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list-all")]
        public IEnumerable<ClientRequest> ListAllClients()
        {
            return _clientService.GetClients();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetClientById(string id)
        {
            try
            {
                //------------------------------------------------------------------------------------------------
                // CHECK PARMS
                //------------------------------------------------------------------------------------------------
                var clientNumber = Int32.Parse(id);

                //------------------------------------------------------------------------------------------------
                // GET CLIENT
                //------------------------------------------------------------------------------------------------
                var client = _clientService.GetUniqueClient(clientNumber);
                if (client == null) return NoContent();
                else // --- FOUND!
                {
                    return Ok(client);
                }
                    
            }
            catch
            {
                return BadRequest("ERR01 - Internal server error!");
            }
        }

        [Route("{id}/approval-status")]
        [HttpPost]
        public IActionResult CheckApprovalStatus(string id)
        {
            try
            {
                //------------------------------------------------------------------------------------------------
                // CHECK PARMS
                //------------------------------------------------------------------------------------------------
                var clientNumber = Int32.Parse(id);

                //------------------------------------------------------------------------------------------------
                // GET CLIENT
                //------------------------------------------------------------------------------------------------
                var clientApprovalStatus = _clientService.CheckApprovalStatus(clientNumber);
                if (clientApprovalStatus == null) return NoContent();
                else // --- FOUND!
                {
                    return Ok(clientApprovalStatus);
                }

            } catch
            {
                return BadRequest("ERR01 - Internal server error!");
            }
        }
    }
}
