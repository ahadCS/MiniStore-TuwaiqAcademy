using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mini_store.Models;
using mini_store.Data;


namespace mini_store.Controllers;

public class HomeController : Controller
{
   private readonly AppDbContext _context;


   public HomeController(AppDbContext cn)
    {
        _context=cn;
    }
    private static dynamic[] _categories =
    {  
new { Id = 0, Name = "إلكترونيات", Icon = "fa-solid fa-bolt-lightning" },
new { Id = 1, Name = "ملابس", Icon = "fa-solid fa-shirt" },
new { Id = 2, Name = "كتب", Icon = "fas fa-book-open" }
    };
private static dynamic[] _products =
{
new { CategoryId = 0, Name = "هاتف ذكي", Price = 2500, Description = "هاتف ذكي بكاميرا عالية الدقة", Image = "phonesmart.jpg" },
new { CategoryId = 0, Name = "حاسوب محمول", Price = 4500, Description = "حاسوب مخصص للمطورين", Image = "Laptop.jpg" },
new { CategoryId = 0, Name = " شاشة", Price = 1500, Description = "شاشة مخصصة للمطورين", Image = "monitor.jpg" },
new { CategoryId = 1, Name = "قميص قطني", Price = 150, Description = "قميص مريح وصيفي", Image = "shirt.jpg" },
new { CategoryId = 1, Name = "قبعة", Price = 14, Description = "قبعة بيسبول", Image = "cap.png" },
new { CategoryId = 1, Name = "وشاح شتوي", Price = 150, Description = " وشاح محبوك ", Image = "w.png" },
new { CategoryId = 2, Name = "كتاب برمجة", Price = 45, Description = "دليل شامل لتعلم البرمجة", Image = "Book.png" },
new { CategoryId = 2, Name = " برمجةالاردوينو", Price = 90, Description = "دليل شامل لتعلم البرمجة", Image = "Book1.png" }
};
public IActionResult Index()
{
    ViewBag.CatergoriesList = _categories;
    return View();
}
    [Route("list")]
    public IActionResult Products(int id)
    {
        var filtered=_products.Where(p=> p.CategoryId==id).ToList();
         ViewBag.Filtered=filtered;
        return View();
    }

    public IActionResult Details(string name)
    {

         var filtered=_products.FirstOrDefault(p=> p.Name== name);

         ViewBag.Products=filtered;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
