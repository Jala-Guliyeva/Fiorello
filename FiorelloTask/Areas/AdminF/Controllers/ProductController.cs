using FiorelloTask.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FiorelloTask.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    public class ProductController : Controller
    {
    private AppDbContext _context;
            public ProductController(AppDbContext context)
            {
                _context = context;

            }
            public IActionResult Index()
        {
            return View();
        }
    }
}
