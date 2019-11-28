using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace SecurityDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //1. configure nlog
            NLogBuilder.ConfigureNLog("nlog.config");
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                //2. configure nlog
                .UseNLog();
    }
}
