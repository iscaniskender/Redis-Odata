using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Redis.InMemoryApp.Models;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Redis.InMemoryApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache memoryCache;
        public ProductController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            //1.Yol
            //if(string.IsNullOrEmpty(memoryCache.Get<string>("date")))
            //{
            //    memoryCache.Set<string>("date", DateTime.Now.ToString());
            //}

            //2.Yol

            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

            //cache ömrü
            options.AbsoluteExpiration = DateTime.Now.AddSeconds(30);

            //cache slide etme ömrü
            options.SlidingExpiration = TimeSpan.FromSeconds(5);

            //silinme önem sırasıdır
            options.Priority = CacheItemPriority.Low;

            //neden silindiğini öğrenebilirsin
            options.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                memoryCache.Set("callback", $"{key}-> {value} => sebep:{reason}");
            });

            memoryCache.Set<string>("date", DateTime.Now.ToString(), options);

            Product p = new Product { Id = 1, Name = "kalem", Price = 200 };

            memoryCache.Set<Product>("product:1", p);


            return View();
        }

        public IActionResult Show()
        {
            //eğer key varsa bak yoksa oluştur

            //memoryCache.GetOrCreate<string>("date", entry =>
            //{
            //    return DateTime.Now.ToString();
            //});


            //memoryCache.Remove("date");

            memoryCache.TryGetValue("date", out string datecache);
            memoryCache.TryGetValue("callback", out string callback);
            memoryCache.TryGetValue("product:1", out Product product);

            ViewBag.date = datecache;
            ViewBag.callback = callback;
            ViewBag.product = product;

            return View();
        }
    }
}
