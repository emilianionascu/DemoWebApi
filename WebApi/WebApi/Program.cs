using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApi.Server;

namespace WebApi
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var host = BuildWebHost(args);
                ProcessDbCommands.Process(args, host);

                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
