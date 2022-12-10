using APIOdata.API.Model;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
