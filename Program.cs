using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ActivityTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {

            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("app.settings.json")
                            .AddCommandLine(args)
                            .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:5000")
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
