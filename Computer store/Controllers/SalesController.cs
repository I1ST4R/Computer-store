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

    public async Task<IActionResult> Index()
    {
        var sales = await _context.Sales
            .Include(p => p.Product)
            .Include(p => p.User)
            .ToListAsync();
        return View(sales);
    }

}