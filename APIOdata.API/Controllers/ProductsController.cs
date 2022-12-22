using System.Linq;
using APIOdata.API.Model;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [ODataRoute("({Item})")]
        [EnableQuery]
        public IActionResult GetUrun([FromODataUri] int Item)
        {
            return Ok(appDbContext.Products.Where(x => x.Id == Item));
        }

        [HttpPost]
        public IActionResult PostProduct([FromBody]Product product)
        {
            appDbContext.Add(product);
            appDbContext.SaveChanges();
            return Ok(product);
        }

        [HttpPut]
        public IActionResult PutProduct([FromODataUri] int key,[FromBody] Product product)
        {
            product.Id = key;
            appDbContext.Entry(product).State = EntityState.Modified;
            appDbContext.SaveChanges();

            return Ok(product);
        }

        [HttpDelete]
        public IActionResult DeleteProduct([FromODataUri] int key)
        {
            var product = appDbContext.Products.Find(key);

            if (product != null) return NotFound();

            appDbContext.Products.Remove(product);
            appDbContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult LoginUser(ODataActionParameters parameters)
        {
            Login login = parameters["UserLogin"] as Login;

            return Ok(login.Name + "-" + login.Password);

        }

        [HttpGet]
        public IActionResult MultiplyFunction([FromODataUri] int a1,int a2, int a3)
        {
            var total = (a1 * a2 * a3);
            return Ok(total);

        }
    }
}
