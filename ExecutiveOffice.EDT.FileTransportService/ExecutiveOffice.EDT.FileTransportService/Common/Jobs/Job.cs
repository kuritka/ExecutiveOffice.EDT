using System;
using FileOps.Pipe;
using System.IO;
using FileOps;

namespace ExecutiveOffice.EDT.FileTransportService.Common.Jobs
{
    internal class Job : IJob
    {
        public void Run()
        {
            DirectoryInfo configurationPath = new DirectoryInfo(Path.Combine("Configuration", "LOCAL", "JobSettings"));

            var csv = new FileInfo(Path.Combine(configurationPath.FullName, "csv.settings.json"));

            var shared = new FileInfo(Path.Combine(configurationPath.FullName, "shared.settings.json"));


            var steps = new FileOpsBuilder()
             .AddConfiguration(csv)
             .AddConfiguration(shared)
             .Build();

            IFileOpsManager manager = new FileOpsManager(steps,"identifer");

            manager.OnStart = (agg) => Console.Write($"Job {agg.Guid} started..");
            manager.OnStepProcessed = (agg) => Console.WriteLine("Step processed");
            manager.OnEnd = (agg) => Console.Write($"{agg.Guid} done");

            manager.Execute();
        }
    }
}
