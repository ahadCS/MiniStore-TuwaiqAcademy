using Microsoft.AspNetCore.Mvc;
using mini_store.Data;
using mini_store.Models;
using Microsoft.EntityFrameworkCore;

namespace mini_store.Controllers
{
    public class ProductsDetailsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsDetailsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: عرض كل تفاصيل المنتجات
        public IActionResult Index()
        {
            var productDetails = _context.ProductDetails.Include(p => p.Product).ToList();
            return View(productDetails);
        }

        // GET: عرض صفحة إنشاء تفصيل منتج جديد
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        // POST: حفظ تفصيل منتج جديد
        [HttpPost]
        public IActionResult Create(ProductDetails details)
        {
            if (details.ImageFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string extension = Path.GetExtension(details.ImageFile.FileName);
                string uniqueFileName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                details.Image = uniqueFileName;

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    details.ImageFile.CopyTo(stream);
                }
            }

            if (ModelState.IsValid)
            {
                _context.ProductDetails.Add(details);
                _context.SaveChanges();
                return RedirectToAction("Index", "ProductsDetails");
            }

            ViewBag.Products = _context.Products.ToList();
            return View(details);
        }

        // GET: عرض صفحة التعديل
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var details = _context.ProductDetails.Find(id);
            if (details == null) return NotFound();

            ViewBag.Products = _context.Products.ToList();
            return View(details);
        }

        // POST: حفظ التعديلات
        [HttpPost]
        public IActionResult Edit(ProductDetails details)
        {
            var existing = _context.ProductDetails.Find(details.Id);
            if (existing == null) return NotFound();

            if (details.ImageFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string extension = Path.GetExtension(details.ImageFile.FileName);
                string uniqueFileName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    details.ImageFile.CopyTo(stream);
                }

                existing.Image = uniqueFileName;
            }

            existing.Description = details.Description;
            existing.MadeIn = details.MadeIn;
            existing.StockQuantity = details.StockQuantity;
            existing.ProductId = details.ProductId;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: حذف تفاصيل منتج
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var details = _context.ProductDetails.Find(id);
            if (details != null)
            {
                _context.ProductDetails.Remove(details);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}