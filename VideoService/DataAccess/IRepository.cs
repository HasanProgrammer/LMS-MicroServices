using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    /*Fetch*/
    public partial interface IRepository<TModel> where TModel : class
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
        
        /*-----------------------------------------------------------*/
        
        public List<TModel> FindAllEagerLoading();
        public IEnumerable<TModel> FindAllAsEnumerableEagerLoading();
        public Task<List<TModel>> FindAllEagerLoadingAsync();
        
        public List<TModel> FindAllActiveEagerLoading();
        public IEnumerable<TModel> FindAllActiveAsEnumerableEagerLoading();
        public Task<List<TModel>> FindAllActiveEagerLoadingAsync();
        
        public List<TModel> FindAllAsNoTrackingEagerLoading();
        public IEnumerable<TModel> FindAllAsNoTrackingAsEnumerableEagerLoading();
        public Task<List<TModel>> FindAllAsNoTrackingEagerLoadingAsync();
        
        public List<TModel> FindAllActiveAsNoTrackingEagerLoading();
        public IEnumerable<TModel> FindAllActiveAsNoTrackingAsEnumerableEagerLoading();
        public Task<List<TModel>> FindAllActiveAsNoTrackingEagerLoadingAsync();
        
        public TModel FindWithIdEagerLoading(object id);
        public Task<TModel> FindWithIdEagerLoadingAsync(object id);
        public TModel FindWithIdActiveEagerLoading(object id);
        public Task<TModel> FindWithIdActiveEagerLoadingAsync(object id);
        public TModel FindWithIdAsNoTrackingEagerLoading(object id);
        public Task<TModel> FindWithIdAsNoTrackingEagerLoadingAsync(object id);
        public TModel FindWithIdActiveAsNoTrackingEagerLoading(object id);
        public Task<TModel> FindWithIdActiveAsNoTrackingEagerLoadingAsync(object id);
    }
    
    /*CRUD*/
    public partial interface IRepository<TModel>
    {
        public bool Add(TModel model);
        public Task<bool> AddAsync(TModel model);

        public bool Change(TModel model, object id);
        public Task<bool> ChangeAsync(TModel model, object id);

        public bool Remove(object id);
        public Task<bool> RemoveAsync(object id);
    }
}