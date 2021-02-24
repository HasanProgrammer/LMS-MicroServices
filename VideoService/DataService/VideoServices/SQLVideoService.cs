using DataAccess.CustomRepositories;
using DataModel;

namespace DataService.VideoServices
{
    public class SQLVideoService : VideoRepository<Video>
    {
        public SQLVideoService()
        {
            
        }
    }
}