using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace APIOdata.API.Controllers
{
    public class HelperController : ODataController
    {
        [ODataRoute("GetKdv")]

    public IActionResult GetKdv()
        {

            return Ok(18);
        }
    }
}
