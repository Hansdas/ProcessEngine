using Microsoft.EntityFrameworkCore;
using ProcessEngine.Domain;
using ProcessEngine.EF;
using ProcessEngine.Maps;
using ProcessEngine.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProcessEngine.Repository
{
  public  class BaseRepository<T,V>: IBaseRepository<T,V> where T:Entity<V>,new()
    {
        protected DBContext _context;
        /// <summary>
        /// 是否降序
        /// </summary>
        protected bool desc = true;
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
        public IEnumerable<T> SelectByPage(int pageIndex, int pageSize, Expression<Func<T, bool>> where = null)
        {
            return GetQueryable(where).Skip((pageIndex-1)*pageSize).Take(pageSize);
        }

        public int SelectCount(Expression<Func<T, bool>> where = null)
        {
           return GetQueryable(where).Count();
        }

        public T SelectSingle(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> orderBy = null)
        {
            T entity = GetQueryable(where).FirstOrDefault();
            return entity;
        }
        public void Update(T t)
        {
            _context.Entry(t).State = EntityState.Modified;
            _context.Entry(t).Property(s => s.Id).IsModified = false;
            _context.SaveChanges();
        }
        public void DeleteById(V id)
        {
            T entity = new T() { Id = id };
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }
        public void Delete(Expression<Func<T, bool>> where = null)
        {
            IEnumerable<T> results = Select(where);
            _context.RemoveRange(results);
            _context.SaveChanges();
        }
        protected IQueryable<T> GetQueryable(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> orderBy = null)
        {
            IQueryable<T> result = _context.Set<T>().AsNoTracking();
            if (where != null)
                result= result.Where(where);
            if (orderBy != null)
                result =desc==true?result.OrderByDescending(orderBy):result.OrderBy(orderBy);
            return result;
        }
    }
}
