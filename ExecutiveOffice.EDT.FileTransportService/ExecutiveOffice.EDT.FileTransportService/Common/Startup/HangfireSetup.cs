using ExecutiveOffice.EDT.FileTransportService.Common.Jobs;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using System;

namespace ExecutiveOffice.EDT.FileTransportService.Common.Startup
{
    internal static class HangfireSetup
    {
        public static IApplicationBuilder SetupHangfire(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            RecurringJob.AddOrUpdate<Job>(
                "abc",
                d => d.Run(),
                "*/5 * * * *");

            RecurringJob.Trigger("abc");


            //if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            //if (configuration.JobSettings == null) throw new ArgumentNullException(nameof(configuration.JobSettings), "No JobSettings configuration file found.");

            //foreach (KeyValuePair<string, JobSettings> jobSettings in configuration.JobSettings.OrderByDescending(d => d.Key))
            //{
            //    string jobIdentifier = jobSettings.Key;

            //    RecurringJob.AddOrUpdate<ChannelJob>(
            //        jobIdentifier,
            //        d => d.Run(jobSettings.Value, configuration.ApplicationSettings),
            //        jobSettings.Value.Cron);

            //    if (jobSettings.Value.TriggerOnStart)
            //    {
            //        RecurringJob.Trigger(jobIdentifier);
            //    }
            //}
            return app;
        }

    }
}
