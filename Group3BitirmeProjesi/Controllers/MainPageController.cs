using Group3BitirmeProjesi.DAL.DbContext;
using Group3BitirmeProjesi.DAL.Entities.Concrete;
using Group3BitirmeProjesi.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group3BitirmeProjesi.Controllers
{
    public class MainPageController : Controller
    {
        private readonly IGenericRepository<Product> _pRepo;
        private readonly IGenericRepository<Category> _CRepo;
        private readonly BitProjeDbContext _context;


        public MainPageController(BitProjeDbContext context, IGenericRepository<Product> pRepo, IGenericRepository<Category> cRepo)
        {
            _context = context;
            _pRepo = pRepo;
            _CRepo = cRepo;
        }

        // Ana Sayfa (Index) ve Kategoriler ile Ürünlerin Görüntülenmesi
        public async Task<IActionResult> Index()
        {
            List<Category> categoriesWithProducts = await _context.Categories
                .Include(c => c.Products) // Kategorilerin ürünlerini dahil et
                .ToListAsync();

            return View(categoriesWithProducts);
        }
   
    }
}
