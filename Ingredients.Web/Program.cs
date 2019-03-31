using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Ingredients.Web.Cache;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Ingredients.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel((config, options) => {
                    if (config.HostingEnvironment.IsDevelopment()) {
                        options.Listen(IPAddress.Loopback, 5000);
                    } else {
                        options.Listen(IPAddress.Any, 80);
                    }
                })
                .UseStartup<Startup>();

    }
}
