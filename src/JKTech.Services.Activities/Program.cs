﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JKTech.Common.Commands;
using JKTech.Common.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JKTech.Services.Activities
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
            .UseRabbitMq()
            .SubscribeToCommand<CreateActivity>()
            .Build()
            .Run();
        }


    }
}
