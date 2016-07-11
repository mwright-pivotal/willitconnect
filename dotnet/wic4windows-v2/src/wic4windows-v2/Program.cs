using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Collections;

namespace wic4windows_v2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();
            IWebHost host;
            IDictionary dict = System.Environment.GetEnvironmentVariables();
            foreach ( DictionaryEntry entry in dict)
            {
                System.Console.Out.WriteLine(entry.Key+":"+entry.Value);
            }
            string sPort = System.Environment.GetEnvironmentVariable("PORT");
            if (sPort != null & sPort.Length > 0)
            {
                System.Console.Out.WriteLine("Detected PORT env parm");
                host = new WebHostBuilder()
                  .UseConfiguration(config)
                  .UseKestrel()
                  .UseContentRoot(Directory.GetCurrentDirectory())
                  .UseUrls("http://0.0.0.0:" + sPort)
                  .UseStartup<Startup>()
                  .Build();
            }
            else {
                host = new WebHostBuilder()
                  .UseConfiguration(config)
                  .UseKestrel()
                  .UseContentRoot(Directory.GetCurrentDirectory())
                  .UseStartup<Startup>()
                  .Build();
            }

            host.Run();
        }
    }
}
