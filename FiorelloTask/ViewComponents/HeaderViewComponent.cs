using FiorelloTask.DAL;
using FiorelloTask.Models;
using FiorelloTask.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloTask.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private AppDbContext _context;
        public HeaderViewComponent(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            int totalCount = 0;
            if (Request.Cookies["basket"]!=null)
            {
                List<BasketProduct> products = JsonConvert.DeserializeObject<List<BasketProduct>>
                  (Request.Cookies["basket"]);

                foreach (var item in products)
                {
                    totalCount += item.Count;
                }

            }

            ViewBag.BasketLength = totalCount;
            Bio bio = _context.Bios.FirstOrDefault();
            return View(await Task.FromResult(bio));
        }
    }
}
