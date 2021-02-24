using System;
using System.Threading.Tasks;
using Common;
using DataAccess;
using DataModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace DataService.RabbitMQServices
{
    public class UserUpdatedListenerService : RabbitMQListener
    {
        private readonly IServiceProvider     _Provider;
        private readonly Config.MongoDatabase _Database;
        
        public UserUpdatedListenerService
        (
            IOptions<Config.RabbitMQ>      rabbit,
            IOptions<Config.Queues>        queue,
            IOptions<Config.MongoDatabase> database,
            IServiceProvider               Provider
        )
        : base(rabbit)
        {
            QueueName = queue.Value.UserUpdatedInfo; /*اسم Queue کانال ارتباطی RabbitMQ ؛ برای اطلاع از تغییرات اطلاعات کاربر*/
            _Provider = Provider;
            _Database = database.Value;
        }

        /*این متد به محض دریافت Message از Queue مربوطه در کانال ارتباطی ، عملیات زیر را انجام میدهد*/
        protected override Task<bool> ActionAsync(string message)
        {
            throw new Exception();
        }
    }
}