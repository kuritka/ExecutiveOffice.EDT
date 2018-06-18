using Microsoft.Extensions.DependencyInjection;

namespace ExecutiveOffice.EDT.FileTransportService.Common.Startup
{
    public static class CorsSetup
    {
        public static IServiceCollection SetupCors(this IServiceCollection services)
        {
            services.AddCors(options => {

                options.AddPolicy("AllowAll",
                        builder => builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());


                options.AddPolicy("AnyGET",
                    builder =>
                        builder.AllowAnyHeader()
                            .WithMethods("GET")
                            .AllowAnyOrigin());
            }
            );
            return services;
        }
    }

}
