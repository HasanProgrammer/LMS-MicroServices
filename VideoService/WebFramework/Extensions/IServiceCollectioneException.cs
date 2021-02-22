using Common;
using DataAccess.CustomRepositories;
using DataModel;
using DataService;
using DataService.RabbitMQServices;
using DataService.VideoServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using WebFramework.Filters;
using VideoService = WebFramework.Services.VideoService;

namespace WebFramework.Extensions
{
    public static class IServiceCollectioneException
    {
        public static void AddConfiguresToServiceContainer(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<Config.StatusCode>   (configuration.GetSection("StatusCode"));
            service.Configure<Config.Messages>     (configuration.GetSection("Messages"));
            service.Configure<Config.MongoDatabase>(configuration.GetSection("MongoDatabase"));
            service.Configure<Config.RabbitMQ>     (configuration.GetSection("RabbitMQ"));
            service.Configure<Config.Queues>       (configuration.GetSection("RabbitMQ.Queues"));
        }
        
        public static void AddMongoDatabaseToServiceContainer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSingleton(new MongoClient(configuration.GetSection("MongoDatabase:ConnectionString").Value));
        }

        public static void AddRabbitMQToServiceContainer(this IServiceCollection service, IConfiguration configuration)
        {
            /*Consumers*/
            service.AddHostedService<UserUpdatedListenerService>();
            /*Consumers*/

            /*Producers*/
            /*Producers*/
        }
        
        public static void AddIocServiceContainer(this IServiceCollection service)
        {
            /*Video's Services*/
            service.AddScoped<VideoRepository<Video>, MongoVideoService>();
            /*Video's Services*/
        }

        public static void AddFiltersToServiceContainer(this IServiceCollection service)
        {
            /*Global*/
            service.AddTransient<ModelValidation>();
            /*Global*/
        }

        public static void AddGlobalObjectsToServiceContainer(this IServiceCollection service)
        {
            /*Video's Service*/
            service.AddScoped<VideoService>();
            /*Video's Service*/
        }
    }
}