using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SerilogTest.Interface.Service;
using SerilogTest.Service;
using Serilog;
using System;
using SerilogTest.Models;
using Serilog.Context;
using System.IO;

namespace SerilogTest
{
    class Program
    {
        private static ServiceProvider serviceProvider;
        static void Main(string[] args)
        {
            SetupDI();
            LogContext.PushProperty("UserId", "70915");//改成驗證時候增加
            var testService = serviceProvider.GetService<ITestService>();
            testService.TestLog();
            testService.TestLog2();
            //SLog();
            Console.WriteLine("end");
            Console.ReadLine();
        }

        private static void SetupDI()
        {
            Environment.SetEnvironmentVariable("MyAppBaseDir", AppDomain.CurrentDomain.BaseDirectory);
            Environment.SetEnvironmentVariable("MyAppSubPath", DateTime.Now.ToString("yyyy-MM-dd"));

            var configurationRoot = ReadFromAppSettings();
            serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configurationRoot)
                .AddScoped<ITestService, TestService>()
                .AddLogging(log => log.AddSerilog(dispose: true))
                .BuildServiceProvider();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configurationRoot).CreateLogger();
        }

        private static void SLog()
        {
            var log = new LoggerConfiguration()
               .WriteTo.Console()
               .CreateLogger();

            log.Information("Hello, Serilog!");
            log.Debug("This is debug");
            log.Error("Something error");
            log.Fatal("This is fatal");

            // var
            var count = 456;
            log.Information("Retrieved {Count} records", count);

            // Collections 
            var fruit = new[] { "Apple", "Pear", "Orange" };
            log.Information("In my bowl I have {Fruit}", fruit);

            var user = new User { Created = DateTime.Now, Id = 2, Name = "Dio" };
            log.Information($"In my bowl I have {@user}", user);
        }

        public static IConfigurationRoot ReadFromAppSettings()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", true)
                .Build();
        }
    }
}
