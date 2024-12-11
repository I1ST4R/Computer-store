using Computer_store.Domain.Entities;
using ComputerStore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class SalesController : Controller
{
    private readonly ComputerStoreContext _context;

    public SalesController(ComputerStoreContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string userLogin)
    {
        var sales = _context.Sales.Include(s => s.Product).Include(s => s.User).AsQueryable();

        if (!string.IsNullOrEmpty(userLogin))
        {
            sales = sales.Where(s => s.User.Login == userLogin);
        }

        return View(sales.ToList());
    }

}