using System;
using System.Collections.Generic;
using System.Text;
using Medusa.Base.Data;

namespace Medusa.Base.Business
{
    public abstract class AbstractBusiness<TDao, TDomain, idTDomain> where TDao : IDao<TDomain, idTDomain>
    {
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

    }
}
