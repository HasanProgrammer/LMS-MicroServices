using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IRepository<TModel> where TModel : class
    {
        public List<TModel> FindAll();
        public IEnumerable<TModel> FindAllAsEnumerable();

        public Task<List<TModel>> FindAllAsync();
        
        /*-----------------------------------------------------------*/

        public List<TModel> FindAllActive();
        public IEnumerable<TModel> FindAllActiveAsEnumerable();

        public Task<List<TModel>> FindAllActiveAsync();
        
        /*-----------------------------------------------------------*/

        public List<TModel> FindAllAsNoTracking();
        public IEnumerable<TModel> FindAllAsNoTrackingAsEnumerable();
        
        public Task<List<TModel>> FindAllAsNoTrackingAsync();
        
        /*-----------------------------------------------------------*/

        public List<TModel> FindAllActiveAsNoTracking();
        public IEnumerable<TModel> FindAllActiveAsNoTrackingAsEnumerable();

        public Task<List<TModel>> FindAllActiveAsNoTrackingAsync();
        
        /*-----------------------------------------------------------*/

        public TModel FindWithId(object id);
        public Task<TModel> FindWithIdAsync(object id);
        public TModel FindWithIdActive(object id);
        public Task<TModel> FindWithIdActiveAsync(object id);
        public TModel FindWithIdAsNoTracking(object id);
        public Task<TModel> FindWithIdAsNoTrackingAsync(object id);
        public TModel FindWithIdActiveAsNoTracking(object id);
        public Task<TModel> FindWithIdActiveAsNoTrackingAsync(object id);
    }
}