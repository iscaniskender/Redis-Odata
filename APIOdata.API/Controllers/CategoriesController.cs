using APIOdata.API.Model;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace APIOdata.API.Controllers
{
    [Route("odata/Categories")]
    public class CategoriesController : ODataController
    {
        private readonly AppDbContext appDbContext;

        public CategoriesController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;   
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        { 
            return Ok(appDbContext.Categories.ToList());
        }
        [EnableQuery]
        public IActionResult GetCategory([FromODataUri] int key)
        {
            return Ok(appDbContext.Products.Where(x => x.Id == key));
        }

        [HttpPost]
        public IActionResult TotalProductPrice([FromODataUri] int key)
        {
            var total = appDbContext.Products.Where(x => x.CategoryId == key).Sum(x => x.Price);
            return Ok(total);
        }

        [HttpPost]
        public IActionResult TotalProductPrice2()
        {
            var total = appDbContext.Products.Sum(x => x.Price);
            return Ok(total);
        }

        [HttpPost]
        public IActionResult TotalProductPriceWithParameter(ODataActionParameters parameters)
        {
            int categoryId = (int)parameters["categoryId"];
            var total = appDbContext.Products.Where(x => x.CategoryId == categoryId).Sum(x => x.Price);
            return Ok(total);
        }

        [HttpPost]
        public IActionResult Total(ODataActionParameters parameters)
        {
            int a = (int)parameters["a"];
            int b = (int)parameters["b"];
            int c = (int)parameters["c"];
            return Ok(a+b+c);
        }


        [HttpGet]
        public IActionResult CategoryCount()
        {
            var total = appDbContext.Categories.Count();
            return Ok(total);
        }
    }
}
