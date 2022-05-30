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
        public IActionResult LoadMore(int skip)
        {
            List<Product> products = _context.Products.Include(p => p.Category).Skip(skip).Take(2).ToList();
            return PartialView("_ProductPartial", products);
        }
    }
}
