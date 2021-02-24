using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.CustomRepositories;
using DataModel;
using Microsoft.EntityFrameworkCore;

namespace DataService.VideoServices
{
    public class SQLVideoService : VideoRepository<Video>
    {
        private readonly DatabaseContext _Context;
        
        public SQLVideoService(DatabaseContext Context)
        {
            _Context = Context;
        }

        public override async Task<List<Video>> FindAllAsNoTrackingAsync()
        {
            return await _Context.Videos.AsNoTracking().ToListAsync();
        }

        public override async Task<List<Video>> FindAllForUserAsNoTrackingAsync(string id)
        {
            return await _Context.Videos.AsNoTracking().Where(Video => Video.UserId.Equals(id)).ToListAsync();
        }

        public override async Task<Video> FindWithTitleAsNoTrackingAsync(string title)
        {
            return await _Context.Videos.AsNoTracking().Where(Video => Video.Title.Equals(title)).FirstOrDefaultAsync();
        }

        public override async Task<bool> AddAsync(Video model)
        {
            await _Context.Videos.AddAsync(model);
            return Convert.ToBoolean(await _Context.SaveChangesAsync());
        }

        public override async Task<bool> ChangeAsync(Video model, object id)
        {
            _Context.Videos.Update(model);
            return Convert.ToBoolean(await _Context.SaveChangesAsync());
        }

        public override async Task<bool> RemoveAsync(Video model)
        {
            _Context.Videos.Remove(model);
            return Convert.ToBoolean(await _Context.SaveChangesAsync());
        }
    }
}