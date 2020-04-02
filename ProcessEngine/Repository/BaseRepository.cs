using Microsoft.EntityFrameworkCore;
using ProcessEngine.Domain;
using ProcessEngine.Maps;
using ProcessEngine.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProcessEngine.Repository
{
  public  class BaseRepository<T,V>: IBaseRepository<T,V> where T:Entity<V>
    {
        private DBContext _context;
        public BaseRepository (DBContext context)
        {
            _context = context;
        }

        public T Insert(T t)
        {
            T entity=  _context.Set<T>().Add(t).Entity;
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<T> Select(Expression<Func<T, bool>> where = null)
        {
            IQueryable<T> list = GetQueryable(where);
            return list.AsEnumerable();
        }

        public T SelectById(V id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> SelectByPage(int pageIndex, int pageSize, Expression<Func<T, bool>> where = null)
        {
            return GetQueryable(where).Skip((pageIndex-1)*pageSize).Take(pageSize);
        }

        public int SelectCount(Expression<Func<T, bool>> where = null)
        {
           return GetQueryable(where).Count();
        }

        public T SelectSingle(Expression<Func<T, bool>> where = null)
        {
            throw new NotImplementedException();
        }

        private IQueryable<T> GetQueryable(Expression<Func<T, bool>> where = null)
        {
            IQueryable<T> result = _context.Set<T>().AsNoTracking();
            if (where != null)
                return result.Where(where);
            return result;
        }
    }
}
