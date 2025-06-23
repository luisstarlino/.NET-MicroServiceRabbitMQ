using MicroRabbit.Banking.Api.Controllers.Core;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MicroRabbit.Banking.Api.Controllers
{
    [ApiController]
    [Route("dashboard")]
    public class DashboardController : BaseController
    {
        [Route("cards")]
        [HttpPost]
        public async Task<IActionResult> GetCardsInformation()
        {
            try
            {

                return;
            } catch (Exception ex)
            {
                return CreateBaseResponse(HttpStatusCode.InternalServerError, "ERR01-Error to get Cards Information.Please, contact the Banking IT Suport");

            }
        }
    }
}
