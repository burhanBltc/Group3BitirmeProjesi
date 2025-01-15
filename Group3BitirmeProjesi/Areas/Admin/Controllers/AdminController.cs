using Microsoft.AspNetCore.Mvc;

namespace Group3BitirmeProjesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        
        
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
