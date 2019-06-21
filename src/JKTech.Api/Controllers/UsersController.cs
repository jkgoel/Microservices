using System.Threading.Tasks;
using JKTech.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace JKTech.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IBusClient _busClient;
        public UsersController(IBusClient busClient)
        {
            _busClient = busClient;

        }

        [HttpGet]
        public IActionResult Get() => Content("Welcome to user service");
        

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] CreateUser command)
        {
            await _busClient.PublishAsync(command);
            return Accepted();
        }
    }
}