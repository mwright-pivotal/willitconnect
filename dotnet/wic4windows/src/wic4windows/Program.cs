using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace wic4windows
{
    public class Program
    {
        public static void Main(string[] args)
        {
            String sPort = System.Environment.GetEnvironmentVariable("PORT");
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseUrls("http://0.0.0.0:" + sPort)
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
