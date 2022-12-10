using System.Linq;
using APIOdata.API.Model;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIOdata.API.Controllers
{
    [Route("odata/Products")]
    [ODataRoutePrefix(("Products"))]
    public class ProductsController : ODataController
    {
        private readonly AppDbContext appDbContext;

        public ProductsController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(appDbContext.Products);
        }

        [EnableQuery]
        public IActionResult GetProduct([FromODataUri] int key)
        {
            return Ok(appDbContext.Products.Where(x => x.Id == key));
        }

        [ODataRoute("Products({Item})")]
        [EnableQuery]
        public IActionResult GetUrun([FromODataUri] int Item)
        {
            return Ok(appDbContext.Products.Where(x => x.Id == Item));
        }
    }
}
