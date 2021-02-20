using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace WebFramework.Extensions.ServiceContainer
{
    public static class MongoDatabase
    {
        public static void AddMongoDatabaseToServiceContainer(this IServiceCollection service, IConfiguration configuration)
        { 
            service.AddSingleton(new MongoClient(configuration.GetSection("MongoDatabase:ConnectionString").Value));
        }
    }
}