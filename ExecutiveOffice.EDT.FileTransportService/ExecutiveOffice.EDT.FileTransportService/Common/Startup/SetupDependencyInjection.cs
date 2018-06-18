using Microsoft.Extensions.DependencyInjection;

namespace ExecutiveOffice.EDT.FileTransportService.Common.Startup
{
    internal static class DependencyInjectionSetup
    {
        public static IServiceCollection SetupDependencyInjection(this IServiceCollection services)
        {
            //services.AddTransient<IConfigurationFactory, ConfigurationFactory>();

            //services.AddSingleton<IBeamerConfiguration, BeamerConfiguration>();

            //services.AddSingleton<IConfigurationBuilder, ConfigurationBuilder>();

            ////services.AddSingleton<IConfigurationFileWatcher, ConfigurationFileWatcher>();

            //services.AddTransient<ChannelJob>();

            ////services.AddTransient<Zip>(
            ////    c => new Zip(new Infrastructure.Compression.Zip(), c.GetService<IBeamerConfiguration>()));

            //services.AddTransient<IStepFactory, StepFactory>();

            //services.AddTransient<IPipeBuilder, PipeBuilder>();

            //services.AddTransient<IFailNotificationFactory, FailNotificationFactory>();

            services.BuildServiceProvider();

            return services;
        }
    }

}
