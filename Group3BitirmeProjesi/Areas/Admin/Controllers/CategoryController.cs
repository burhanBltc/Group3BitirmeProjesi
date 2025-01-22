using Group3BitirmeProjesi.DAL.Entities.Concrete;
using Group3BitirmeProjesi.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Identity.Client;

namespace Group3BitirmeProjesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {

        private readonly IGenericRepository<Product> _prepo;
        private readonly IGenericRepository<Category> _crepo;
        public CategoryController(IGenericRepository<Product> prepo, IGenericRepository<Category> crepo)
        {
            _prepo = prepo;
            _crepo = crepo;
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Category = await _crepo.GetAllAsync();
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {

            if (ModelState.IsValid)
            {
                await _crepo.AddAsync(category);
                return RedirectToAction(nameof(List));
            }
            ViewBag.Categorie = await _crepo.GetAllAsync();
            return View(category);

        }

        public async Task<IActionResult> List()
        {
            var category = await _crepo.GetAllAsync();
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _crepo.GetByIdAsync(id);
            if(category == null)
            {
                return NotFound();

            }
            category.ModdifiedDate = DateTime.Now;
            ViewBag.Categories = await _crepo.GetAllAsync();
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
                category.ModdifiedDate = DateTime.Now;
                await _crepo.UpdateAsync(category);
                
                return RedirectToAction(nameof(List));
            }
            ViewBag.Category = await _crepo.GetAllAsync();
            return View(category);  
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _crepo.GetByIdAsync(id);
            if(category == null)
            {
                return NotFound();

            }
            await _crepo.DeleteAsync(id);
            return RedirectToAction(nameof(List));        
             
        }


    }
}
