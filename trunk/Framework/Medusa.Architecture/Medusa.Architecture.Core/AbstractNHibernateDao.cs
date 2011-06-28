using System;
using System.Collections.Generic;
using NHibernate;
using Mds.Architecture.Utils;
using log4net;
using NHibernate.Criterion;

namespace Mds.Architecture.Data
{
    public abstract class AbstractNHibernateDao<T, IdT> : IDao<T, IdT>
    {
        private Type persitentType = typeof(T);
        protected readonly string SessionFactoryConfigPath;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().GetType());

        public static ILog Log
        {
            get { return AbstractNHibernateDao<T, IdT>.log; }
        } 

        /// <param name="sessionFactoryConfigPath">Fully qualified path of the session factory's config file</param>
        public AbstractNHibernateDao(string sessionFactoryConfigPath) {

            Check.Require(! string.IsNullOrEmpty(sessionFactoryConfigPath),
                "sessionFactoryConfigPath may not be null nor empty");

            SessionFactoryConfigPath = sessionFactoryConfigPath;
        }

        /// <summary>
        /// Loads an instance of type T from the DB based on its ID.
        /// </summary>
        public T GetById(IdT id, bool shouldLock) {
            
            T entity;

            try
            {
                if (shouldLock)
                {
                    entity = (T)NHibernateSession.Load(persitentType, id, LockMode.Upgrade);
                }
                else
                {
                    entity = (T)NHibernateSession.Load(persitentType, id);
                }
            }
            finally
            {
                NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
            }
            return entity;
        }

        /// <summary>
        /// Loads every instance of the requested type with no filtering.
        /// </summary>
        public List<T> GetAll() {
            return GetByCriteria();
        }

        /// <summary>
        /// Loads every instance of the requested type using the supplied <see cref="ICriterion" />.
        /// If no <see cref="ICriterion" /> is supplied, this behaves like <see cref="GetAll" />.
        /// </summary>
        public List<T> GetByCriteria(params ICriterion[] criterion) {

            try
            {
                ICriteria criteria = NHibernateSession.CreateCriteria(persitentType);

                foreach (ICriterion criterium in criterion)
                {
                    criteria.Add(criterium);
                }
                return criteria.List<T>() as List<T>;
            }
            finally
            {
                NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
            }     
        }

        /// <summary>
        /// Looks for a single instance using the example provided.
        /// </summary>
        /// <param name="exampleInstance"></param>
        /// <param name="propertiesToExclude"></param>
        /// <returns></returns>
        public List<T> GetByExample(T exampleInstance, params string[] propertiesToExclude) {

            List<T> list;
            try
            {
                ICriteria criteria = NHibernateSession.CreateCriteria(persitentType);
                Example example = Example.Create(exampleInstance);

                foreach (string propertyToExclude in propertiesToExclude)
                {
                    example.ExcludeProperty(propertyToExclude);
                }

                criteria.Add(example);
                list = criteria.List<T>() as List<T>;
            }
            finally
            {
                NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
            }
            return list;
        }

        /// <summary>
        /// Looks for a single instance using the example provided.
        /// </summary>
        /// <exception cref="NonUniqueResultException" />
        public T GetUniqueByExample(T exampleInstance, params string[] propertiesToExclude) {

            List<T> foundList;
            try
            {
                foundList = GetByExample(exampleInstance, propertiesToExclude);
            }
            finally
            {
                NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
            }

            if (foundList.Count > 1)
            {
                throw new NonUniqueResultException(foundList.Count);
            }

            if (foundList.Count > 0)
            {
                return foundList[0];
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// For entities that have assigned ID's, you must explicitly call Save to add a new one.
        /// See http://www.hibernate.org/hib_docs/reference/en/html/mapping.html#mapping-declaration-id-assigned.
        /// </summary>
        public T Save(T entity) {

            using (ITransaction trans = NHibernateSession.BeginTransaction())
            {
                try
                {
                    NHibernateSession.Save(entity);
                    NHibernateSession.Flush();
                    trans.Commit();
                }
                catch (HibernateException)
                {
                    trans.Rollback();
                    NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
                    throw;  
                }
                finally
                {
                    NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
                }
            }
            return entity;
        }

        /// <summary>
        /// For entities with automatatically generated IDs, such as identity, SaveOrUpdate may 
        /// be called when saving a new entity.  SaveOrUpdate can also be called to _update_ any 
        /// entity, even if its ID is assigned.
        /// </summary>
        public T SaveOrUpdate(T entity) {

            using (ITransaction trans = NHibernateSession.BeginTransaction())
            {
                try
                {
                    NHibernateSession.SaveOrUpdate(entity);
                    NHibernateSession.Flush();
                    trans.Commit();
                }
                catch (HibernateException)
                {
                    trans.Rollback();
                    NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
                    throw;  
                }
                finally
                {
                    NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
                }
            }
            return entity;
        }

        /// <summary>
        /// Delete an entity from database
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity) {
                    
            using (ITransaction trans = NHibernateSession.BeginTransaction())
            {
                try
                {
                   NHibernateSession.Delete(entity);
                   NHibernateSession.Flush();
                   trans.Commit();                  
                }
                catch (HibernateException)
                {  
                    trans.Rollback();
                    NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
                    throw;     
                }
                finally 
                {
                    NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
                }              
            }  
        }

        /// <summary>
        /// Commits changes regardless of whether there's an open transaction or not
        /// </summary>
        public void CommitChanges() {
            if (NHibernateSessionManager.Instance.HasOpenTransactionOn(SessionFactoryConfigPath)) {
                NHibernateSessionManager.Instance.CommitTransactionOn(SessionFactoryConfigPath);
            }
            else{
                // If there's no transaction, just flush the changes
                NHibernateSessionManager.Instance.GetSessionFrom(SessionFactoryConfigPath).Flush();
            }
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Update(T entity)
        {
            using (ITransaction trans = NHibernateSession.BeginTransaction())
            {
                try
                {
                    NHibernateSession.Update(entity);
                    NHibernateSession.Flush();
                    trans.Commit();                    
                }
                catch (HibernateException)
                {
                    trans.Rollback();
                    NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
                    throw;
                }
                finally
                {
                    NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
                }
                return entity;
            }
        }

        /// <summary>
        /// Find an entity based on condition string
        /// </summary>
        /// <param name="conditionString"></param>
        /// <returns></returns>
        public List<T> Find(string conditionString)
        {
            List<T> list;
            try
            {
                if (string.IsNullOrEmpty(conditionString))
                    list = (List<T>)NHibernateSession.CreateCriteria(typeof(T)).List<T>();
                else
                    list = (List<T>)NHibernateSession.CreateQuery(string.Format("From {0} e where {1}", typeof(T).Name, conditionString)).List<T>();
            }
            finally
            {
                NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
            }
            return list;
        }

        /// <summary>
        /// Execute an HQL Query
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public List<T> ExecuteQuery(string queryString)
        {
            List<T> list;
            try
            {
                if (string.IsNullOrEmpty(queryString))
                    list = (List<T>)NHibernateSession.CreateCriteria(typeof(T)).List<T>();
                else
                    list = (List<T>)NHibernateSession.CreateQuery(queryString).List<T>();
            }
            finally
            {
                NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
            }
            return list;
        }

        /// <summary>
        /// Execute an HQL query defined in hbm.xml file
        /// </summary>
        /// <param name="queryName"></param>
        /// <returns></returns>
        public List<T> ExecuteNamedQuery(string queryName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Execute a SQL Query
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public List<T> ExecuteSQLQuery(string queryString)
        {
            List<T> list;
            try
            {
                if (string.IsNullOrEmpty(queryString))
                    list = (List<T>)NHibernateSession.CreateCriteria(typeof(T)).List<T>();
                else
                    list = (List<T>)NHibernateSession.CreateSQLQuery(queryString).List<T>();
            }
            finally
            {
                NHibernateSessionManager.Instance.CloseSessionOn(SessionFactoryConfigPath);
            }
            return list;
        }

        /// <summary>
        /// Execute a named SQL Query
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public List<T> ExecuteNamedSQLQuery(string queryString)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Exposes the ISession used within the DAO.
        /// </summary>
        protected ISession NHibernateSession
        {
            get
            {
                return NHibernateSessionManager.Instance.GetSessionFrom(SessionFactoryConfigPath);
            }
        }

    }
}
