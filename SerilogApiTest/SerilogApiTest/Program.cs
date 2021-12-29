using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace SerilogApiTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("MyAppBaseDir", AppDomain.CurrentDomain.BaseDirectory);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     var env = hostingContext.HostingEnvironment;
                     config.SetBasePath(env.ContentRootPath)
                           .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                           .AddJsonFile(path: $"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                     //°Ñ¦Ò:
                     //https://github.com/serilog/serilog-sinks-elasticsearch
                     //https://stackoverflow.com/questions/52046747/different-minimum-level-logs-serilog
                     Log.Logger = 
                     new LoggerConfiguration().ReadFrom.Configuration(config.Build())
                     //.WriteTo.File("my-app.log", hooks: new ArchiveHooks(CompressionLevel.NoCompression, @"C:\Logs"))
                     .CreateLogger();
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
