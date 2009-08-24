//using System;
//using System.Collections.Generic;
//using Castle.ActiveRecord;
//using NHibernate.Impl;
//using ar = Castle.ActiveRecord;
//using NHibernate;
//using Suricato.NHibernate;


//namespace Suricato.ActiveRecord
//{
//    public class ActiveRecordRepository<T, TId> : IRepository<T, TId> where T : class 
//    {
//        #region IRepository<T,TId> Members

//        public ISession Session {
//            get { throw new NotImplementedException(); }
//        }

//        public T Get(TId id) {
//            return ActiveRecordMediator<T>.FindByPrimaryKey(id);
//        }

//        public T Load(TId id) {
//            return Get(id);
//        }

//        public void Delete(T entity) {
//            ActiveRecordMediator<T>.Delete(entity);
//        }

//        public void DeleteAll() {
//            ActiveRecordMediator<T>.DeleteAll();
//        }

//        public T Save(T entity) {
//            ActiveRecordMediator<T>.Create(entity);
//            return entity;
//        }

//        public T SaveOrUpdate(T entity) {
//            ActiveRecordMediator.Save(entity);
//            return entity;
//        }

//        public T SaveOrUpdateCopy(T entity) {
//            return ActiveRecordMediator<T>.SaveCopy(entity);
//        }

//        public void Update(T entity) {
//            ActiveRecordMediator.Update(entity);
//        }

//        public IList<T> FindAll(DetachedQuery dq) {
//            return ActiveRecordMediator<T>.FindAll(dq);
//        }

//        public T FindOne(DetachedQuery dq) {
//            return ActiveRecordMediator<T>.FindOne(dq);
//        }

//        public IList<T> SlicedFindAll(int firstResult, int maxResults, IDetachedQuery detachedQuery) {
//            return ActiveRecordMediator<T>.SlicedFindAll(firstResult, maxResults, detachedQuery);
//        }

//        #endregion
//    }
//}