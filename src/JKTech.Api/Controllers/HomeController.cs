using Microsoft.AspNetCore.Mvc;

namespace JKTech.Api.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Get() => Content("Hello from JKTech API");

        

    }
}