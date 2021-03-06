using Common;
using DataAccess;
using DataAccess.CustomRepositories;
using DataModel;
using DataService.CacheServices;
using DataService.ChapterServices;
using DataService.RabbitMQServices;
using DataService.VideoServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StackExchange.Redis;
using WebFramework.Filters;
using VideoService = WebFramework.Services.WebSPA.VideoService;

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
            service.Configure<Config.Redis>        (configuration.GetSection("Redis"));
        }
        
        public static void AddSQLDatabaseToServiceContainer(this IServiceCollection service, IConfiguration configuration)
        {
            string ConnectionString = configuration.GetConnectionString("SQL Server");
            service.AddDbContext<DatabaseContext>(Config => Config.UseSqlServer(ConnectionString), ServiceLifetime.Singleton);
        }
        
        public static void AddMongoDatabaseToServiceContainer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSingleton(new MongoClient(configuration.GetSection("MongoDatabase:ConnectionString").Value));
        }

        public static void AddRedisCacheToServiceContainer(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSingleton<IConnectionMultiplexer>(ServiceProvider => ConnectionMultiplexer.Connect(configuration.GetValue<string>("Redis:Connection")));
            service.AddSingleton<ICacheService, CacheService>();
        }

        public static void AddRabbitMQToServiceContainer(this IServiceCollection service, IConfiguration configuration)
        {
            /*Consumers*/
            service.AddHostedService<UserUpdatedListenerService>();
            service.AddHostedService<ChapterUpdatedListenerService>();
            /*Consumers*/

            /*Producers*/
            service.AddSingleton<Common.RabbitMQ>();
            /*Producers*/
        }
        
        public static void AddIocServiceContainer(this IServiceCollection service)
        {
            /*Video's Services*/
            service.AddScoped<VideoRepository<Video>, SQLVideoService>();
            /*Video's Services*/
            
            /*Chapter's Service*/
            service.AddScoped<ChapterRepository<Chapter>, SQLChapterService>();
            /*Chapter's Service*/
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