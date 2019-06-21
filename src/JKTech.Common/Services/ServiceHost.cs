using System;
using JKTech.Common.Commands;
using JKTech.Common.Events;
using JKTech.Common.RabbitMq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;

namespace JKTech.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webhost;

        public ServiceHost(IWebHost webHost)
        {
            _webhost = webHost;
        }

        public void Run() => _webhost.Run();

        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;
            var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();
            var webHostBuilder = WebHost.CreateDefaultBuilder()
            .UseConfiguration(config)
            .UseStartup<TStartup>();
            return new HostBuilder(webHostBuilder.Build());
        }

        public abstract class BuilderBase
        {
            public abstract ServiceHost Build();
        }

        public class HostBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBusClient _bus;
            public HostBuilder(IWebHost webHost)
            {
                _webHost = webHost;

            }

            public BusBuilder UseRabbitMq()
            {
                _bus = (IBusClient)_webHost.Services.GetService(typeof(IBusClient));
                return new BusBuilder(_webHost, _bus);
            }
            public override ServiceHost Build()
            {
                throw new NotImplementedException();
            }
        }

        public class BusBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBusClient _bus;
            public BusBuilder(IWebHost webHost, IBusClient bus)
            {
                _webHost = webHost;
                _bus = bus;

            }

            public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
            {
                using (var serviceScope = _webHost.Services.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var handler = (ICommandHandler<TCommand>)serviceScope.ServiceProvider.GetService(typeof(ICommandHandler<TCommand>));
                    _bus.WithCommandHanderAsync(handler);
                    return this;
                }
              
            }

            public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
            {
                using (var serviceScope = _webHost.Services.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var handler = (IEventHandler<TEvent>)serviceScope.ServiceProvider.GetService(typeof(IEventHandler<TEvent>));
                    _bus.WithCommandHanderAsync(handler);
                    return this;
                }
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }

    }
}