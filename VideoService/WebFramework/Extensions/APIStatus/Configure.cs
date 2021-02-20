using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebFramework.Extensions.APIStatus
{
    public static class Configure
    {
        public static void AddAPIStatusToConfigure(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<Config.StatusCode>(configuration.GetSection("StatusCode"));
            service.Configure<Config.Messages>  (configuration.GetSection("Messages"));
        }
    }
}