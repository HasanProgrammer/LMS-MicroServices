using System;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.CustomRepositories;
using DataModel;
using Microsoft.EntityFrameworkCore;

namespace DataService.ChapterServices
{
    public class SQLChapterService : ChapterRepository<Chapter>
    {
        private readonly DatabaseContext _Context;
        
        public SQLChapterService(DatabaseContext Context)
        {
            _Context = Context;
        }

        public override async Task<Chapter> FindWithIdAsNoTrackingAsync(object id)
        {
            return await _Context.Chapters.AsNoTracking().FirstOrDefaultAsync(Chapter => Chapter.Id.Equals(id.ToString()));
        }

        public override async Task<bool> AddAsync(Chapter model)
        {
            await _Context.Chapters.AddAsync(model);
            return Convert.ToBoolean(await _Context.SaveChangesAsync());
        }
    }
}