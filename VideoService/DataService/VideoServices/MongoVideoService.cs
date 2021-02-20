using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataAccess.CustomRepositories;
using DataModel;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataService.VideoServices
{
    /*Configs*/
    public partial class MongoVideoService : VideoRepository<Video>
    {
        private readonly IMongoCollection<Video> _Videos;
        
        public MongoVideoService(MongoClient client, IOptions<Config.MongoDatabase> options)
        {
            _Videos = client.GetDatabase(options.Value.DatabaseName).GetCollection<Video>(options.Value.VideosCollectionName);
        }
    }

    /*Fetch*/
    public partial class MongoVideoService
    {
        public override async Task<List<Video>> FindAllAsync()
        {
            var videos = await _Videos.FindAsync(video => true);
            return await videos.ToListAsync();
        }

        public override async Task<List<Video>> FindAllForUserAsync(string username)
        {
            var videos = await _Videos.FindAsync(video => video.User.Username.Equals(username));
            return await videos.ToListAsync();
        }
    }
    
    /*CRUD*/
    public partial class MongoVideoService
    {
        public override async Task<bool> AddAsync(Video model)
        {
            await _Videos.InsertOneAsync(model);
            return true;
        }
    }
}