using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.CustomRepositories
{
    public abstract class RoleRepository<TModel> : IRepository<TModel> where TModel : class
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
    }
}