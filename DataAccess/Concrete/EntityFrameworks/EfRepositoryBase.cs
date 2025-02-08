using Microsoft.EntityFrameworkCore;
using SignalR.DataAccess.Abstract;
using SignalR.DataAccess.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Core.DataAccess.Concrete.EntityFramework
{
    public class EfRepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class, new()
    {
        protected readonly SignalRContext _context;

        public EfRepositoryBase(SignalRContext context)
        {
            _context = context;
        }

        public List<TEntity> GetAllData(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = query.Include(includes);
            }

            return query.ToList();
        }

        public TEntity GetData(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = query.Include(includes);
            }

            return query.FirstOrDefault();
        }
        public void AddData(TEntity entity)
        {
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            _context.SaveChanges();
        }

        public void UpdateData(TEntity entity)
        {
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool DeleteData(int id)
        {
            var value = _context.Set<TEntity>().Find(id);

            if (value != null)
            {
                var deletedEntity = _context.Entry(value);
                deletedEntity.State = EntityState.Deleted;
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public IQueryable<TEntity> Queryable()
        {
            return _context.Set<TEntity>();
        }

        public bool DeleteDataByGuid(Guid id)
        {
            var value = _context.Set<TEntity>().Find(id);

            if (value != null)
            {
                var deletedEntity = _context.Entry(value);
                deletedEntity.State = EntityState.Deleted;
                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}