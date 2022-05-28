using FiorelloTask.DAL;
using FiorelloTask.Models;
using FiorelloTask.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FiorelloTask.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context=context;

        }
        public IActionResult Index()
        {
            HomeVM homeVm = new HomeVM();
            homeVm.sliders= _context.Sliders.ToList();
            homeVm.pageIntro= _context.PageIntros.FirstOrDefault();
            homeVm.categories = _context.Categories.ToList();
            return View(homeVm);
        }
    }
}
