using FiorelloTask.DAL;
using FiorelloTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FiorelloTask.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            ViewBag.ProductCount=_context.Products.Count();
            List<Product> products = _context.Products.Include(p => p.Category).Take(2).ToList();
            return View(products);
        }
      public IActionResult SearchProduct(string search)
        {
            List<Product>products=_context.Products.Where(p=>p.Name.ToLower().Contains(search.ToLower())).Take(10).ToList();
            return PartialView("_Search",products);
        
        }
    }
}
