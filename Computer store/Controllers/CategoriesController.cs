using Computer_store.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

public class CategoriesController : Controller
{
    private static List<Category> _categories = new List<Category>
    {
    };

    public IActionResult Index()
    {
        return View(_categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Back()
    {
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (_categories.Any())
        {
            category.Id = _categories.Max(p => p.Id) + 1;
        }
        else
        {
            category.Id = 1;
        }

        _categories.Add(category);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var category = _categories.FirstOrDefault(p => p.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        var existingCategory = _categories.FirstOrDefault(p => p.Id == category.Id);
        if (existingCategory == null)
        {
            return NotFound();
        }
        existingCategory.Name = category.Name;
        existingCategory.Description = category.Description;
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var category = _categories.FirstOrDefault(p => p.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        var category = _categories.FirstOrDefault(p => p.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        _categories.Remove(category);
        return RedirectToAction("Index");
    }
}
