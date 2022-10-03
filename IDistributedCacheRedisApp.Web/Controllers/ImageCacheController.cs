using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.IO;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class ImageCacheController : Controller
    {
        private IDistributedCache distributedCache;

        public ImageCacheController(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;

        }
        public IActionResult ImageCache()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sergen.jpg");
            byte[] imageByte = System.IO.File.ReadAllBytes(path);
            distributedCache.Set("sergenImage", imageByte);
            return View();
        }

        public IActionResult GetCache()
        {
            byte[] imageByte = distributedCache.Get("sergenImage");

            return File(imageByte,"image/jpg");
        }
        public IActionResult Index()
        {
            return View();
        }



    }
}
