using System.Collections.Generic;
using NHibernate;
using NHibernate.Impl;


namespace Suricato.NHibernate
{
    public interface IRepository<T, TId>
    {
        ISession Session { get; }
        T Get(TId id);
        T Load(TId id);
        void Delete(T entity);
        void DeleteAll();
        T Save(T entity);
        T SaveOrUpdate(T entity);
        T SaveOrUpdateCopy(T entity);
        void Update(T entity);
        IList<T> FindAll(DetachedQuery dq);
        T FindOne(DetachedQuery dq);
        IList<T> SlicedFindAll(int firstResult, int maxResults, IDetachedQuery detachedQuery);
    }
}