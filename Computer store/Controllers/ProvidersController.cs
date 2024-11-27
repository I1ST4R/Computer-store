using Computer_store.Domain.Entities;
using ComputerStore.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ProvidersController : Controller
{
    private readonly ComputerStoreContext _context;

    public ProvidersController(ComputerStoreContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var providers = await _context.Providers.ToListAsync();
        return View(providers);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Provider provider)
    {
        _context.Providers.Add(provider);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var provider = await _context.Providers.FindAsync(id);
        if (provider == null)
        {
            return NotFound();
        }
        return View(provider);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Provider provider)
    {
        _context.Entry(provider).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var provider = await _context.Providers.FindAsync(id);
        if (provider == null)
        {
            return NotFound();
        }
        return View(provider);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var provider = await _context.Providers.FindAsync(id);
        _context.Providers.Remove(provider);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
