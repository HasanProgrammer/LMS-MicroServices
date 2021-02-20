using DataAccess.CustomRepositories;
using DataModel;
using DataService.VideoServices;
using Microsoft.Extensions.DependencyInjection;
using VideoService = DataService.VideoServices.Service;

namespace WebFramework.Extensions.ServiceContainer
{
    public static class PureServices
    {
        public static void AddPureServicesToServiceContainer(this IServiceCollection service)
        {
            /*-------------------------------------------------------IOC-------------------------------------------------------*/
            
            /*Video's Services*/
            service.AddScoped<VideoRepository<Video>, MongoVideoService>();
            service.AddScoped<VideoService>();
            /*Video's Services*/
            
            /*-------------------------------------------------------IOC-------------------------------------------------------*/
        }
    }
}