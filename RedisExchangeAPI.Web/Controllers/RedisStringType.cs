using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class RedisStringType : Controller
    {
        private readonly RedisService redisService;

        private readonly IDatabase db;
        public RedisStringType(RedisService redisService)
        {
            this.redisService = redisService;
            db = redisService.GetDb(0);
        }
        public IActionResult Index()
        {
            db.StringSet("name", "iskender");
            db.StringSet("visitor", 100);
            return View();
        }
    }
}
