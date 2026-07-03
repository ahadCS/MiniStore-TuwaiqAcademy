using Microsoft.AspNetCore.Mvc;
using mini_store.Models;
using mini_store.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace mini_store.Controllers
{    [Authorize]
     public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("dashboard")]
        public IActionResult Index(string searchTerm)
        {
            ViewBag.categories = _context.Categories.ToList();
            ViewBag.CurrentSearch = searchTerm;

            var query = _context.Products.Include(p => p.Categories).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm));
            }

            var products = query.ToList();
            return View(products);
        }


        [HttpPost]
        public IActionResult Create(Product product)
        {   if (string.IsNullOrEmpty(Request.Form["CategoryId"]))
          {   
            ModelState.Remove("CategoryId");
            ModelState.AddModelError("CategoryId", "يجب اختيار الفئة");
         }
            if (product.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "يجب اختيار صورة");
            }

            if (product.ImageFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string extension = Path.GetExtension(product.ImageFile.FileName);
                string uniqueFileName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                product.Images = uniqueFileName;
                ModelState.Remove(nameof(Product.Images));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    product.ImageFile.CopyTo(stream);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            
          ViewBag.categories = _context.Categories.ToList();
          ViewBag.SubmittedProduct = product; 
          var products = _context.Products.Include(p => p.Categories).ToList();
          return View("Index", products);
             }

        // 4. حذف المنتج
        public IActionResult Delete(int id)
        {
            var products = _context.Products.Find(id);
            if (products != null)
            {
                _context.Products.Remove(products);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // 5.   (عرض)
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var products = _context.Products.Find(id);
            ViewBag.categories = _context.Categories.ToList();

            if (products == null) return NotFound();
            return View(products);
        }

        // 6.   (حفظ)
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            ModelState.Remove(nameof(Product.ImageFile));

            if (!ModelState.IsValid)
            {
                ViewBag.categories = _context.Categories.ToList();
                return View(product);
            }
            var existingProduct = _context.Products.Find(product.Id);
            if (existingProduct == null) return NotFound();

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Images = product.Images;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}