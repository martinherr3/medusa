using System;
using System.Collections.Generic;
using System.Text;

namespace Mds.Architecture.Data
{
    public interface IDao<T, IdT>
    {
        T GetById(IdT id, bool shouldLock);
        List<T> GetAll();
        List<T> GetByExample(T exampleInstance, params string[] propertiesToExclude);
        T GetUniqueByExample(T exampleInstance, params string[] propertiesToExclude);
        T Save(T entity);
        T SaveOrUpdate(T entity);
        T Update(T entity);
        void Delete(T entity);
        void CommitChanges();
        List<T> Find(string conditionString);
        List<T> ExecuteQuery(string queryString);
        List<T> ExecuteNamedQuery(string queryName);
        List<T> ExecuteSQLQuery(string queryString);
        List<T> ExecuteNamedSQLQuery(string queryName);
    }
}
