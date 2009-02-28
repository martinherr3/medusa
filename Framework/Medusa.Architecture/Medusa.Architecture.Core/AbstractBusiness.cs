using System;
using System.Collections.Generic;
using System.Text;
using Medusa.Architecture.Data;
using log4net;

namespace Medusa.Architecture.Business
{
    public abstract class AbstractBusiness<TDao, TDomain, idTDomain> where TDao : IDao<TDomain, idTDomain>
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().GetType());
        private TDao dao;

        public AbstractBusiness(TDao pdao)
        {
            dao = pdao;
        }

        /// <summary>
        /// Get one entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TDomain GetById(idTDomain id)
        {
            return dao.GetById(id, true);
        }

        /// <summary>
        /// Get one entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TDomain GetById(idTDomain id, bool shouldLock)
        {
            return dao.GetById(id, shouldLock);
        }

        /// <summary>
        /// Get all entities.
        /// </summary>
        /// <returns></returns>
        public List<TDomain> GetAll()
        {
            return dao.GetAll();
        }

        /// <summary>
        /// Insert an entity into database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TDomain Insert(TDomain entity)
        {
            return dao.Save(entity);
        }

        /// <summary>
        /// Delete an entity from database
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TDomain entity)
        {
            dao.Delete(entity);
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TDomain Update(TDomain entity)
        {
            return dao.SaveOrUpdate(entity);
        }

        /// <summary>
        /// Commit the changes to the database
        /// </summary>
        public void Commit()
        {
            dao.CommitChanges();
        }

        public static ILog Log
        {
            get { return AbstractBusiness<TDao, TDomain, idTDomain>.log; }
        }

        public TDao Dao
        {
            get { return dao; }
            set { dao = value; }
        }
    }
}
