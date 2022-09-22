using Microsoft.AspNetCore.Mvc;

namespace Redis.InMemoryApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
