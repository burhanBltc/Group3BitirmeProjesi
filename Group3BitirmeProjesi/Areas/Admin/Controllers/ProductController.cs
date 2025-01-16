using Group3BitirmeProjesi.DAL.DbContext;
using Group3BitirmeProjesi.DAL.Entities.Concrete;
using Group3BitirmeProjesi.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group3BitirmeProjesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class ProductController : Controller
    {
        private readonly IGenericRepository<Product> _pRepo;
        private readonly IGenericRepository<Category> _CRepo;
        private readonly BitProjeDbContext _context;

        public ProductController(IGenericRepository<Product> pRepo, IGenericRepository<Category> cRepo, BitProjeDbContext context)
        {
            _pRepo = pRepo;
            _CRepo = cRepo;
            _context = context;
        }

        // GET: Admin/Product/Create
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            // Kategorileri alarak dropdown gibi bir seçim yapmak için ViewBag 
            ViewBag.Categories = await _CRepo.GetAllAsync();
            return View();
        }

        // POST: Admin/Product/Create
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _pRepo.AddAsync(product);
                return RedirectToAction(nameof(List));
            }
            ViewBag.Categories = await _CRepo.GetAllAsync();
            return View(product);
        }

        // GET: Admin/Product/List
        public async Task<IActionResult> List()
        {
            var products = await _pRepo.GetAllAsync();
      
            // Category ilişkisini dahil etmek için, her bir ürünü category ile birlikte almak için:
            var productsWithCategory = await _context.Products
                                                      .Include(p => p.Category) // Category ilişkisini yükle
                                                      .ToListAsync();

            return View(productsWithCategory); // İlişkili veriyi view'a gönder
        }

        // GET: Admin/Product/Update/5
        public async Task<IActionResult> Update(int id)
        {
            var product = await _pRepo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = await _CRepo.GetAllAsync();
            return View(product);
        }

        // POST: Admin/Product/Update/5
        [HttpPost]

        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _pRepo.UpdateAsync(product);
                return RedirectToAction(nameof(List));
            }

            ViewBag.Categories = await _CRepo.GetAllAsync();
            return View(product);
        }

        // POST: Admin/Product/Delete/5
       
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _pRepo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _pRepo.DeleteAsync(id);
            return RedirectToAction(nameof(List));
        }

    }
}
