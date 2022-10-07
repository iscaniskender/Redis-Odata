using APIOdata.API.Model;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace APIOdata.API.Controllers
{
    [Route("odata/Categories")]
    public class CetegoriesController : ODataController
    {
        private readonly AppDbContext appDbContext;

        public CetegoriesController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;   
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        { 
            return Ok(appDbContext.Categories.ToList());
        }
    }
}
