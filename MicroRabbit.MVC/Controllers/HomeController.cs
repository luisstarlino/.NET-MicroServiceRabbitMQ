using MicroRabbit.MVC.Models;
using MicroRabbit.MVC.Models.DTO;
using MicroRabbit.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MicroRabbit.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransferService _transferService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ITransferService transferService)
        {
            _logger = logger;
            _transferService = transferService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(TransferViewModel m)
        {
            TransferDTO transferDTO = new TransferDTO()
            {
                FromAccount = m.FromAccount,
                ToAccount = m.ToAccount,
                TransferAmount = m.TransferAmount
            };

            await _transferService.Transfer(transferDTO);

            return View("Index");
        }
    }
}
