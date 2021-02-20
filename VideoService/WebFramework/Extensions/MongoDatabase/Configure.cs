using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebFramework.Extensions.MongoDatabase
{
    public static class Configure
    {
        public static void AddMongoDatabaseToConfigure(this IServiceCollection service, IConfiguration configuration)
        { 
            service.Configure<Config.MongoDatabase>(configuration.GetSection("MongoDatabase"));
        }
    }
}