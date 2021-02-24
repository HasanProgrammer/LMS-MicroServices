using Common;
using DataAccess;
using Microsoft.Extensions.Options;

namespace DataService.RabbitMQServices
{
    public class ChapterUpdatedListenerService : RabbitMQListener
    {
        public ChapterUpdatedListenerService(IOptions<Config.RabbitMQ> options) : base(options)
        {
            
        }
    }
}