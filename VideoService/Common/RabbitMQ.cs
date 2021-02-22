using System;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Common
{
    public class RabbitMQ : IDisposable
    {
        private readonly IConnection _Connection;
        private readonly IModel      _Channel;
        private readonly string      _QueueName;
        
        public RabbitMQ(IOptions<Config.RabbitMQ> rabbit, IOptions<Config.Queues> queue)
        {
            _QueueName = queue.Value.IdentityService;
            
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = rabbit.Value.HostName,
                    UserName = rabbit.Value.Username,
                    Password = rabbit.Value.Password,
                    Port     = rabbit.Value.Port
                };

                _Connection = factory.CreateConnection();
                _Channel    = _Connection.CreateModel();

            }
            catch (Exception e)
            {
                
            }
        }
        
        public void PublishMessage(object payload)
        {
            _Channel.QueueDeclare(queue: _QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _Channel.BasicPublish(exchange: null, routingKey: null, basicProperties: null, body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload)));
        }

        public void Dispose()
        {
            _Connection.Close();
        }
    }
}