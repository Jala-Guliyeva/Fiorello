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
                if (dbProduct.Count<=existBasketProduct.Count)
                {
                    TempData["Fail"] = "not enough count";
                    return RedirectToAction("index", "home");
                }
                else
                {
                    existBasketProduct.Count++;
                }   
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

        public IActionResult RemoveItem(int? id)
        {
            if (id == null) return NotFound();
            string basket = Request.Cookies["basket"];
            List<BasketProduct> products = 
                JsonConvert.DeserializeObject<List<BasketProduct>>(basket);

            BasketProduct existProduct = products.FirstOrDefault(p => p.Id == id);

            if (existProduct == null) return NotFound();
            
            products.Remove(existProduct);

            Response.Cookies.Append("basket",
                JsonConvert.SerializeObject(products),
                new CookieOptions { MaxAge=TimeSpan.FromMinutes(20)});

            return RedirectToAction(nameof(basket));
        }


        public IActionResult Plus(int? id)
        {
            if (id == null) return NotFound();
            string basket = Request.Cookies["basket"];
            List<BasketProduct> products =
                JsonConvert.DeserializeObject<List<BasketProduct>>(basket);

            BasketProduct existProduct = products.FirstOrDefault(p => p.Id == id);

            if (existProduct == null) return NotFound();

            Product dbProduct = _context.Products.FirstOrDefault(p => p.Id == id);

            if (dbProduct.Count>=existProduct.Count)
            {
                existProduct.Count++;
            }
            else
            {
                TempData["Fail"] = "not enough count";
                return RedirectToAction("Basket", "Basket");
            }

            Response.Cookies.Append(
                "basket",
                JsonConvert.SerializeObject(products),
                new CookieOptions { MaxAge = TimeSpan.FromMinutes(20) });

            return RedirectToAction(nameof(Basket));
        }


        public IActionResult Minus(int? id)
        {
            if (id == null) return NotFound();
            string basket = Request.Cookies["basket"];
            List<BasketProduct> products =
                JsonConvert.DeserializeObject<List<BasketProduct>>(basket);

            BasketProduct existProduct = products.FirstOrDefault(p => p.Id == id);

            if (existProduct == null) return NotFound();

            if (existProduct.Count>1)
            {

                existProduct.Count--;
            }
            else
            {
                RemoveItem(existProduct.Id);
                return RedirectToAction(nameof(Basket));
            }

            Response.Cookies.Append(
                "basket",
                JsonConvert.SerializeObject(products),
                new CookieOptions { MaxAge = TimeSpan.FromMinutes(20) });

            return RedirectToAction(nameof(Basket));
        }
    }
}
