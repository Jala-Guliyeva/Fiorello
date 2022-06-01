using FiorelloTask.DAL;
using FiorelloTask.Models;
using FiorelloTask.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloTask.Controllers
{
    public class BasketController : Controller
    {
        private AppDbContext _context;
        public BasketController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddBasket(int?id)
        {   

            if (id == null) return NotFound();
            Product dbProduct = await _context.Products.FindAsync(id);
            if (dbProduct == null) return NotFound();

            List<BasketProduct> products;

            string existBasket = Request.Cookies["basket"];
            if (existBasket == null)
            {

                products = new List<BasketProduct>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketProduct>>(Request.Cookies
                    ["basket"]);
            }
                BasketProduct existBasketProduct = products.FirstOrDefault(p => p.Id==dbProduct.Id);
                if (existBasketProduct==null)
                {
                    BasketProduct basketProduct = new BasketProduct();
                    basketProduct.Id = dbProduct.Id;
                    basketProduct.Name = dbProduct.Name;
                    basketProduct.Count = 1;

                    products.Add(basketProduct);
                }
                else
                {
                    existBasketProduct.Count++;
                }
            

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products), new 
                CookieOptions { MaxAge = TimeSpan.FromMinutes(30)  });
            return RedirectToAction("Index","Home");
        }
        public IActionResult Basket()
        {
           List<BasketProduct> products=JsonConvert.DeserializeObject<List<BasketProduct>>
                (Request.Cookies["basket"]);

            List<BasketProduct> updateProducts = new List<BasketProduct>();
            foreach (var item in products)
            {
                Product dbProduct = _context.Products.FirstOrDefault(p=>p.Id==item.Id);
                BasketProduct basketProduct = new BasketProduct()
                {
                    Id =dbProduct.Id,
                    Price=dbProduct.Price,
                    Name=dbProduct.Name,
                    ImageUrl=dbProduct.ImageUrl,
                    Count=item.Count

                };


                updateProducts.Add(basketProduct);
            }
            return View(updateProducts);
        }
    }
}
