using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.CustomRepositories
{
    /*Main*/
    public abstract partial class ChapterRepository<TModel> : IRepository<TModel> where TModel : class
    {
        public virtual List<TModel> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<TModel> FindAllAsEnumerable()
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<List<TModel>> FindAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public virtual List<TModel> FindAllActive()
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<TModel> FindAllActiveAsEnumerable()
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<List<TModel>> FindAllActiveAsync()
        {
            throw new System.NotImplementedException();
        }

        public virtual List<TModel> FindAllAsNoTracking()
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<TModel> FindAllAsNoTrackingAsEnumerable()
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<List<TModel>> FindAllAsNoTrackingAsync()
        {
            throw new System.NotImplementedException();
        }

        public virtual List<TModel> FindAllActiveAsNoTracking()
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<TModel> FindAllActiveAsNoTrackingAsEnumerable()
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<List<TModel>> FindAllActiveAsNoTrackingAsync()
        {
            throw new System.NotImplementedException();
        }

        public virtual TModel FindWithId(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<TModel> FindWithIdAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual TModel FindWithIdActive(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<TModel> FindWithIdActiveAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual TModel FindWithIdAsNoTracking(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<TModel> FindWithIdAsNoTrackingAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual TModel FindWithIdActiveAsNoTracking(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<TModel> FindWithIdActiveAsNoTrackingAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual List<TModel> FindAllEagerLoading()
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<TModel> FindAllAsEnumerableEagerLoading()
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<List<TModel>> FindAllEagerLoadingAsync()
        {
            throw new System.NotImplementedException();
        }

        public virtual List<TModel> FindAllActiveEagerLoading()
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<TModel> FindAllActiveAsEnumerableEagerLoading()
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<List<TModel>> FindAllActiveEagerLoadingAsync()
        {
            throw new System.NotImplementedException();
        }

        public virtual List<TModel> FindAllAsNoTrackingEagerLoading()
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<TModel> FindAllAsNoTrackingAsEnumerableEagerLoading()
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<List<TModel>> FindAllAsNoTrackingEagerLoadingAsync()
        {
            throw new System.NotImplementedException();
        }

        public virtual List<TModel> FindAllActiveAsNoTrackingEagerLoading()
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<TModel> FindAllActiveAsNoTrackingAsEnumerableEagerLoading()
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<List<TModel>> FindAllActiveAsNoTrackingEagerLoadingAsync()
        {
            throw new System.NotImplementedException();
        }

        public virtual TModel FindWithIdEagerLoading(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<TModel> FindWithIdEagerLoadingAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual TModel FindWithIdActiveEagerLoading(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<TModel> FindWithIdActiveEagerLoadingAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual TModel FindWithIdAsNoTrackingEagerLoading(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<TModel> FindWithIdAsNoTrackingEagerLoadingAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual TModel FindWithIdActiveAsNoTrackingEagerLoading(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<TModel> FindWithIdActiveAsNoTrackingEagerLoadingAsync(object id)
        {
            throw new System.NotImplementedException();
        }
    }
    
    /*Main*/
    public abstract partial class ChapterRepository<TModel>
    {
        public virtual bool Add(TModel model)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<bool> AddAsync(TModel model)
        {
            throw new System.NotImplementedException();
        }

        public virtual bool Change(TModel model, object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<bool> ChangeAsync(TModel model, object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual bool Remove(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<bool> RemoveAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<bool> Remove(TModel model)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<bool> RemoveAsync(TModel model)
        {
            throw new System.NotImplementedException();
        }
    }
    
    /*Custom*/
    public abstract partial class ChapterRepository<TModel>
    {
    }
}