using Computer_store.Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ComputerStore.Domain;
using Computer_store.Models;

namespace ComputerStore.Controllers
{
    public class AccountController : ComputerStoreController
    {

        private readonly ComputerStoreContext _context;

        public AccountController(ComputerStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);

            if (user is null)
            {
                ViewBag.Error = "Некорректные логин и (или) пароль";
                return View("Index", model);
            }

            await AuthenticateAsync(user);
            switch (user.Role)
            {
                case "Seller":
                    return RedirectToAction("Seller", "Home");
                case "Manager":
                    return RedirectToAction("Manager", "Home");
                default:
                    return RedirectToAction("Admin", "Home");
            }
        }

        private async Task AuthenticateAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, user. Id. ToString()),
                new Claim (ClaimsIdentity.DefaultNameClaimType, user. Login),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
            
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
}
