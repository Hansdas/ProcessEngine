﻿using ProcessEngine.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ProcessEngine.Repository.Interface
{
   public interface IBaseRepository<T,V> where T:Entity<V>
    {
        T Insert(T t);
        T SelectById(V id);
        T SelectSingle(Expression<Func<T,bool>> where=null);
        int SelectCount(Expression<Func<T,bool>> where=null);
        IEnumerable<T> Select(Expression<Func<T, bool>> where = null);
        IEnumerable<T> SelectByPage(int pageIndex, int pageSize, Expression<Func<T, bool>> where = null);
    }
}
