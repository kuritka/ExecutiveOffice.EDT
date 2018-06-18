using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ExecutiveOffice.EDT.FileTransportService.Common.Startup
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.SetupCors();

            services.SetupDependencyInjection();

            services.AddHangfire(c => { c.UseMemoryStorage(); });
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            try
            {
                const string applicationStarted = "Application started...";

                app.UseCors("AllowAll");

                app.UseHangfireServer();

                app.UseHangfireDashboard("/hangfire", new DashboardOptions
                {
                    Authorization = new[] { new DefaultAuthorizationFilter() }
                });

                app.Run(async context => await context.Response.WriteAsync($"try http://localhost:{Constants.DefaultHangfirePort}/hangfire"));

                // IBeamerConfiguration configuration = app.ApplicationServices.GetService<IBeamerConfiguration>();

                //app.ApplicationServices.GetService<IConfigurationFileWatcher>();

                app.SetupHangfire();

                Console.WriteLine(applicationStarted);
            }
            catch (Exception )
            {
                //loggerFactory.AuditError(ex.Message).SystemError(ex.Message);
                throw;
            }
        }
    }
}
