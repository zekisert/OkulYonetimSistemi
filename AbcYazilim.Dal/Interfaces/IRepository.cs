﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace AbcYazilim.Dal.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Insert(T entity);
        void Insert(IEnumerable<T> entities);
        void Update(T entity);
        void Update(T entity,IEnumerable<string> fields);
        void Update(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        TResult Find<TResult>(Expression<Func<T, bool>> filter,Expression<Func<T,TResult>> selector);
        IQueryable<TResult> Select<TResult>(Expression<Func<T, bool>> filter,Expression<Func<T,TResult>> selector);
    }
}