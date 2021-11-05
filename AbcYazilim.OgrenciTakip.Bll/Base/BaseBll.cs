using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Controls;
using AbcYazilim.Dal.Interfaces;
using AbcYazilim.OgrenciTakip.Bll.Functions;
using AbcYazilim.OgrenciTakip.Bll.Interfaces;
using AbcYazilim.OgrenciTakip.Model.Entities.Base;

namespace AbcYazilim.OgrenciTakip.Bll.Base
{
    public class BaseBll<T, TContext> : IBaseBll where T : BaseEntity where  TContext : DbContext
    {
        private readonly Control _ctrl;
        private IUnitOfWork<T> _uow;


        protected BaseBll() { }

        protected BaseBll(Control ctrl)
        {
            _ctrl = ctrl;
        }

        protected TResult BaseSingle<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector)
        {
            GeneralFunctions.CreateUnitOfWork<T,TContext>(ref _uow);
            return _uow.Rep.Find(filter, selector);
        }

        protected IQueryable<TResult> BaseList<TResult>(Expression<Func<T, bool>> filter,
            Expression<Func<T, TResult>> selector)
        {
            GeneralFunctions.CreateUnitOfWork<T,TContext>(ref _uow);
            return _uow.Rep.Select(filter, selector);
        }

        protected bool BaseInsert(BaseEntity entity, Expression<Func<T, bool>> filter)
        {
            GeneralFunctions.CreateUnitOfWork<T,TContext>(ref _uow);
            _uow.Rep.Insert(entity.EntityConvert<T>());
            return _uow.Save();
        }

        protected bool BaseUpdate(BaseEntity oldEntity, BaseEntity currentEntity, Expression<Func<T, bool>> filter)
        {
            GeneralFunctions.CreateUnitOfWork<T,TContext>(ref _uow);
            var degisenAlanlar = oldEntity.DegisenAlanlariGetir(currentEntity);
            if (degisenAlanlar.Count == 0) return true;
            _uow.Rep.Update(currentEntity.EntityConvert<T>(),degisenAlanlar);
            return _uow.Save();
        }

        protected bool BaseDelete(BaseEntity entity,)


        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }

                
                disposedValue = true;
            }
        }

        public void Dispose()
        {
           
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }
    }
}