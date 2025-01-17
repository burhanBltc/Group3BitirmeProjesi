using Group3BitirmeProjesi.DAL.Entities.Concrete;
using Group3BitirmeProjesi.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Identity.Client;

namespace Group3BitirmeProjesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class CategoryController : Controller
    {

        private readonly IGenericRepository<Product> _Prepo;
        private readonly IGenericRepository<Category> _Crepo;
        public CategoryController(IGenericRepository<Product> prepo, IGenericRepository<Category> crepo)
        {
            _Prepo = prepo;
            _Crepo = crepo;
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Category = await _Crepo.GetAllAsync();
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {

            if (ModelState.IsValid)
            {
                await _Crepo.AddAsync(category);
                return RedirectToAction(nameof(List));
            }
            ViewBag.Categorie = await _Crepo.GetAllAsync();
            return View(category);

        }

        public async Task<IActionResult> List()
        {
            var category = await _Crepo.GetAllAsync();
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _Crepo.GetByIdAsync(id);
            if(category == null)
            {
                return NotFound();

            }
            ViewBag.Categories = await _Crepo.GetAllAsync();
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Category category,int id)
        {
            if(id != category.Id)
            {

                return NotFound();
            }

            if(ModelState.IsValid)
            {
                await _Crepo.UpdateAsync(category);
                return RedirectToAction(nameof(List));
            }
            ViewBag.Category = await _Crepo.GetAllAsync();
            return View(category);  
        }
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _Crepo.GetByIdAsync(id);
            if(category == null)
            {
                return NotFound();

            }
            await _Crepo.DeleteAsync(id);
            return RedirectToAction(nameof(List));        
             
        }


    }
}
