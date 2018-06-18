using ExecutiveOffice.EDT.FileTransportService.Common.Startup;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace ExecutiveOffice.EDT.FileTransportService
{
    class Program
    {
        internal static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Beamer starting...");
            Console.ResetColor();
            BuildWebHost(args).Run();
        }


        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls($"http://*:{Constants.DefaultHangfirePort}")
                .Build();
    }
}
