﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JKTech.Services.Activities.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            return Content("Hello from JKTech Services Activities API");
        }

    }
}
