using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Redis.InMemoryApp.Models;
using System;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class ComplexTypeController : Controller
    {
        private IDistributedCache distributedCache;

        public ComplexTypeController(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;

        }
        public async Task<IActionResult> Index()
        {
            Product product = new Product { Id = 1, Name = "Pen", Price = 25 };
            string jsonProcut = JsonConvert.SerializeObject(product);


            await distributedCache.SetStringAsync("product:1", jsonProcut);

            Byte[] byteProduct = Encoding.UTF8.GetBytes(jsonProcut);
            distributedCache.Set("product:2",byteProduct);

            return View();
        }

        public async Task<IActionResult> GetCache()
        {
            string jsonData = distributedCache.GetString("product:1");
            var product1 = JsonConvert.DeserializeObject<Product>(jsonData);

            Byte[] byteData = distributedCache.Get("product:2");
            string product2 = Encoding.UTF8.GetString(byteData);
            Product p = JsonConvert.DeserializeObject<Product>(product2);


            return View();
        }
    }
}
