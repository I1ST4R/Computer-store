using Computer_store.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

public class ProvidersController : Controller
{
    private static List<Provider> _providers = new List<Provider>
    {
    };

    public IActionResult Index()
    {
        return View(_providers);
    }
    public IActionResult Back()
    {
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Provider provider)
    {

        if (_providers.Any())
        {
            provider.Id = _providers.Max(p => p.Id) + 1;
        }
        else
        {
            provider.Id = 1;
        }

        _providers.Add(provider);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var provider = _providers.FirstOrDefault(p => p.Id == id);
        if (provider == null)
        {
            return NotFound();
        }
        return View(provider);
    }

    [HttpPost]
    public IActionResult Edit(Provider provider)
    {
        var existingProvider = _providers.FirstOrDefault(p => p.Id == provider.Id);
        if (existingProvider == null)
        {
            return NotFound();
        }
        existingProvider.Name = provider.Name;
        existingProvider.Description = provider.Description;
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var provider = _providers.FirstOrDefault(p => p.Id == id);
        if (provider == null)
        {
            return NotFound();
        }
        return View(provider);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        var provider = _providers.FirstOrDefault(p => p.Id == id);
        if (provider == null)
        {
            return NotFound();
        }
        _providers.Remove(provider);
        return RedirectToAction("Index");
    }
}
