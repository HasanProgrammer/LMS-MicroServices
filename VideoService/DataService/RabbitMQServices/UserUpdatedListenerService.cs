using System.Threading.Tasks;
using Common;
using DataAccess;
using Microsoft.Extensions.Options;

namespace DataService.RabbitMQServices
{
    public class UserUpdatedListenerService : RabbitMQListener
    {
        public UserUpdatedListenerService(IOptions<Config.RabbitMQ> rabbit, IOptions<Config.Queues> queue) : base(rabbit)
        {
            QueueName = queue.Value.IdentityService;
        }

        protected override async Task<bool> ActionAsync(string message)
        {
            return await base.ActionAsync(message);
        }
    }
}