using Computer_store.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class ProductsController : Controller
{
    private static List<Product> _products = new List<Product>
    {
    };

    public IActionResult Index()
    {
        return View(_products);
    }

    public IActionResult Create()
    {
        return View();
    }

    public IActionResult Back()
    {
        return RedirectToAction("Index", "Home");
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (_products.Any())
        {
            product.Id = _products.Max(p => p.Id) + 1;
        }
        else
        {
            product.Id = 1;
        }

        _products.Add(product);
        return RedirectToAction("Index");
    }


    public IActionResult Edit(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct == null)
        {
            return NotFound();
        }
        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.CategoryId = product.CategoryId;
        existingProduct.ProviderId = product.ProviderId;
        existingProduct.Price = product.Price;
        existingProduct.Quantity = product.Quantity;
        return RedirectToAction("Index");
    }

    public IActionResult SellerEdit(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [HttpPost]
    public IActionResult SellerEdit(Product product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct == null)
        {
            return NotFound();
        }
        existingProduct.Quantity -= product.Quantity;
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin,Manager")]
    public IActionResult Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        _products.Remove(product);
        return RedirectToAction("Index");
    }
}
