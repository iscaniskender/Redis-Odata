using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;

namespace IDistributedCacheRedisApp.Web.Controllers
{
   
    public class ProductsController : Controller
    {
        private IDistributedCache distributedCache;

        public ProductsController(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
           
        }
        public IActionResult Index()
        {
            DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.AbsoluteExpiration=DateTime.Now.AddMinutes(5);
            distributedCache.SetString("city", "istanbul",cacheOptions);
            return View();
        }

        public IActionResult Show()
        {
            string name = distributedCache.GetString("city");

            return View();
        }

        public IActionResult Remove()
        {
            distributedCache.Remove("city");

            return View();
        }
    }
}
