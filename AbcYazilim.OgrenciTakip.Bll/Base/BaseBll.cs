using System;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Windows.Controls;
using AbcYazilim.Dal.Interfaces;
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

            return _uow.Rep.Find(filter, selector);
        }
        
        
        
        
        
        
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