using Computer_store.Domain.Entities;
using ComputerStore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class ProductsController : Controller
{
    private readonly ComputerStoreContext _context;

    public ProductsController(ComputerStoreContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Provider)
            .ToListAsync();
        return View(products);
    }

    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Providers = await _context.Providers.ToListAsync();
        return View();
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Providers = await _context.Providers.ToListAsync();

        // Загрузка связанных сущностей
        product.Category = await _context.Categories.FindAsync(product.CategoryId);
        product.Provider = await _context.Providers.FindAsync(product.ProviderId);

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Provider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Providers = await _context.Providers.ToListAsync();
        return View(product);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> Edit(Product product)
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Providers = await _context.Providers.ToListAsync();

        product.Category = await _context.Categories.FindAsync(product.CategoryId);
        product.Provider = await _context.Providers.FindAsync(product.ProviderId);

        _context.Entry(product).State = EntityState.Modified;

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Sale(Product product)
    {
        var existingProduct = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Provider)
            .FirstOrDefaultAsync(p => p.Id == product.Id);

        if (existingProduct == null)
        {
            return NotFound();
        }
        var quantity = existingProduct.Quantity - product.Quantity;
        if (quantity >= 0 && product.Quantity >= 1)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Invalid UserId");
            }

            var sale = new Sale
            {
                ProductId = product.Id,
                UserId = userId,
                Amount = product.Quantity * existingProduct.Price,
                Date = DateTime.Now
            };

            existingProduct.Quantity = quantity;

            _context.Sales.Add(sale);
        }
        else
        {
            quantity = 0;
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Provider)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}