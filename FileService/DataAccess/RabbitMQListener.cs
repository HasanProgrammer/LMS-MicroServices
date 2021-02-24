using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DataAccess
{
    public class RabbitMQListener : IHostedService
    {
        private readonly IConnection _Connection;
        private readonly IModel      _Channel;
        
        /*-----------------------------------------------------------*/

        protected string QueueName;
        
        /*-----------------------------------------------------------*/
        
        public RabbitMQListener(IOptions<Config.RabbitMQ> options)
        {
            try
            {
                /*در این قسمت به سرویس RabbitMQ متصل میشویم*/
                var factory = new ConnectionFactory
                {
                    UserName = options.Value.Username,
                    Password = options.Value.Password,
                    HostName = options.Value.HostName,
                    Port     = options.Value.Port
                };

                _Connection = factory.CreateConnection(); /*برقراری ارتباط با سرور RabbitMQ*/
                _Channel    = _Connection.CreateModel();  /*دسترسی به کانال ارتباطی RabbitMQ در سرور مربوطه*/
            }
            catch (Exception e)
            {
                
            }
        }

        /*در این قسمت عملیاتی که قرار است در ازای دریافت پیام انجام شود ، انجام می گیرید*/
        protected virtual Task<bool> ActionAsync(string message)
        {
            return null;
        }
        
        /*در این متد عملیات اصلی دریافت Message از Service دیگه انجام میشه*/
        private void Listen()
        {
            /*در این قسمت باید Queue موجود در کانال ارتباطی مربوطه در RabbitMQ معرفی گردد*/
            _Channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            
            /*در این قسمت که قسمت اصلی عملیات پردازش Message است ، باید پیام را دریافت و به پردازنده پیام یعنی ActionAsync تحویل داد*/
            var consumer = new EventingBasicConsumer(_Channel);
            consumer.Received += async (sender, args) =>
            {
                if(await ActionAsync(Encoding.UTF8.GetString(args.Body.ToArray())))
                    _Channel.BasicAck(args.DeliveryTag, false);
            };
            _Channel.BasicConsume(queue: QueueName, consumer: consumer);
        }
        
        /*-----------------------------------------------------------ForHostService-----------------------------------------------------------*/

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Listen();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _Connection.Close();
            return Task.CompletedTask;
        }
    }
}