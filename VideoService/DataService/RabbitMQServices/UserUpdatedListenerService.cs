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
            QueueName = queue.Value.IdentityService; /*اسم Queue کانال ارتباطی RabbitMQ ؛ برای اطلاع از تغییرات اطلاعات کاربر*/
            _Provider = Provider;
            _Database = database.Value;
        }

        /*این متد به محض دریافت Message از Queue مربوطه در کانال ارتباطی ، عملیات زیر را انجام میدهد*/
        protected override async Task<bool> ActionAsync(string message)
        {
            /*در این قسمت باید Message دریافت شده را که حاوی اطلاعات کاربر به شکل Json می باشد ، بازیابی کرد*/
            User user = JsonConvert.DeserializeObject<User>(message);
            
            /*در این قسمت اطلاعات کاربر در تمام ردیف های موجود در Video's Collection باید به روز رسانی گردد*/ 
            IMongoCollection<Video> videos = _Provider.GetRequiredService<MongoClient>().GetDatabase(_Database.DatabaseName).GetCollection<Video>(_Database.VideosCollectionName);

            FilterDefinition<Video> video  = Builders<Video>.Filter.Where(item => item.Id.Equals(user.Id));
            UpdateDefinition<Video> fields = Builders<Video>.Update.Set(field => field.User.ImageFile   , user.ImageFile)
                                                                   .Set(field => field.User.Username    , user.Username)
                                                                   .Set(field => field.User.Email       , user.Email)
                                                                   .Set(field => field.User.Phone       , user.Phone)
                                                                   .Set(field => field.User.Expert      , user.Expert)
                                                                   .Set(field => field.User.Description , user.Description);
            
            /*در این قسمت با توجه نتیجه عملیات UPDATE ، خروجی مورد نظر برگشت داده میشود*/
            try
            {
                await videos.UpdateOneAsync(video, fields);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}