using Computer_store.Domain.Entities;
using ComputerStore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        var products = await _context.Products.ToListAsync();
        return View(products);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Providers = await _context.Providers.ToListAsync();
        return View();
    }

    public IActionResult Back()
    {
        return RedirectToAction("Index", "Home");
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Providers = await _context.Providers.ToListAsync();
            return View(product);
        }

        if (!await _context.Categories.AnyAsync(c => c.Id == product.CategoryId))
        {
            ModelState.AddModelError("CategoryId", "Invalid CategoryId.");
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Providers = await _context.Providers.ToListAsync();
            return View(product);
        }

        if (!await _context.Providers.AnyAsync(p => p.Id == product.ProviderId))
        {
            ModelState.AddModelError("ProviderId", "Invalid ProviderId.");
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Providers = await _context.Providers.ToListAsync();
            return View(product);
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Providers = await _context.Providers.ToListAsync();
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Product product)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Providers = await _context.Providers.ToListAsync();
            return View(product);
        }

        if (!await _context.Categories.AnyAsync(c => c.Id == product.CategoryId))
        {
            ModelState.AddModelError("CategoryId", "Invalid CategoryId.");
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Providers = await _context.Providers.ToListAsync();
            return View(product);
        }

        if (!await _context.Providers.AnyAsync(p => p.Id == product.ProviderId))
        {
            ModelState.AddModelError("ProviderId", "Invalid ProviderId.");
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Providers = await _context.Providers.ToListAsync();
            return View(product);
        }

        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> SellerEdit(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> SellerEdit(Product product)
    {
        var existingProduct = await _context.Products.FindAsync(product.Id);
        if (existingProduct == null)
        {
            return NotFound();
        }
        existingProduct.Quantity -= product.Quantity;
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
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