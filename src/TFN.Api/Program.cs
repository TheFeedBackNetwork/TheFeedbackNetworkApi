using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace TFN.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                //.UseWebRoot(Path.Combine(Directory.GetCurrentDirectory(),"build","client"))
                .UseUrls("http://+:5000")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
