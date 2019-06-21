using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JKTech.Common.Commands;
using JKTech.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace JKTech.Services.Identity.Controllers
{
    [Route("{controller}")]
    public class AccountController :Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUser commmand)
            => Json(await _userService.LoginAsync(commmand.Email, commmand.Password));

    }
}
