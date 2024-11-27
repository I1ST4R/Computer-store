using Computer_store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ComputerStore.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class PagesController : Controller
    {
        private readonly ILogger<PagesController> _logger;

        public PagesController(ILogger<PagesController> logger)
        {
            _logger = logger;
        }
        public IActionResult Back()
        {
            return RedirectToAction("Index", "Account");
        }
        public IActionResult Products()
        {
            return RedirectToAction("Index", "Products");
        }
        public IActionResult Categories()
        {
            return RedirectToAction("Index", "Categories");
        }
        public IActionResult Providers()
        {
            return RedirectToAction("Index", "Providers");
        }
        public IActionResult Sales()
        {
            return RedirectToAction("Index", "Sales");
        }
        public IActionResult Users()
        {
            return RedirectToAction("Index", "Users");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

