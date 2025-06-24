using MicroRabbit.Banking.Api.Controllers.Core;
using MicroRabbit.Banking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MicroRabbit.Banking.Api.Controllers
{
    [ApiController]
    [Route("dashboard")]
    public class DashboardController : BaseController
    {
        private readonly IDashboardService _dashboardServices;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardServices = dashboardService;
        }


        [Route("cards")]
        [HttpPost]
        public async Task<IActionResult> GetCardsInformation()
        {
            try
            {
                //------------------------------------------------------------------------------------------------
                // GET INFORMATIONS
                //------------------------------------------------------------------------------------------------
                var cards = await _dashboardServices.GetDashboardCards();

                return Ok(cards);
            } catch (Exception ex)
            {
                return CreateBaseResponse(HttpStatusCode.InternalServerError, "ERR01-Error to get Cards Information.Please, contact the Banking IT Suport");

            }
        }
    }
}
