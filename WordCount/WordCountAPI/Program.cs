using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WordCount.Data;

namespace WordCount
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Contains("-deploy"))
            {
                new DeployDatabaseHelper().Deploy();
            }
            else
            {
                CreateHostBuilder(args).Build().Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}