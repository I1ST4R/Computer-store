using Computer_store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ComputerStore.Controllers
{

    public class PagesController : Controller
    {
        private readonly ILogger<PagesController> _logger;

        public PagesController(ILogger<PagesController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Categories()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Seller,Manager")]
        public IActionResult Products()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Seller")]
        public IActionResult Providers()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Sales()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Users()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

