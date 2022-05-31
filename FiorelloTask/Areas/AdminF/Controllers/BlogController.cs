using FiorelloTask.DAL;
using FiorelloTask.Extentions;
using FiorelloTask.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloTask.Areas.AdminF.Controllers
{

    [Area("AdminF")]
    public class BlogController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Blog> blogs = _context.Blogs.ToList();
            return View(blogs);
        }
       
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Create(Blog blog)
        {
            //validationstate-requiredolanlar
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();

            }

            if (!blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Accept only image!");

                return View();
            }
            if (blog.Photo.ImageSize(10000))
            {
                ModelState.AddModelError("Photo", "1mq yuxari olabilmez!");

                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExistName = _context.Blogs.Any(c => c.Title.ToLower() == blog.Title.ToLower());
            if (isExistName)
            {
                ModelState.AddModelError("Name", "Eyni adlı kategoriya mövcuddur.");
                return View();
            }
            //string path = @"C:\Users\TOSHIBA\Desktop\FiorelloAdminF\FiorelloTask\wwwroot\img\";

            string fileName = await blog.Photo.SaveImage(_env, "img");
            Blog newBlog = new Blog();
            newBlog.Desc=blog.Desc;
            newBlog.Title=blog.Title;
            newBlog.Image = fileName;
            await _context.Blogs.AddAsync(newBlog);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Blog dbBlog = await _context.Blogs.FindAsync(id);
            if (dbBlog == null) return NotFound();
            _context.Blogs.Remove(dbBlog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

