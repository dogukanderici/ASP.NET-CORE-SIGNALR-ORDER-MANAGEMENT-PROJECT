using Microsoft.EntityFrameworkCore;
using SignalR.Core.DataAccess.Concrete.EntityFramework;
using SignalR.DataAccess.Abstract;
using SignalR.DataAccess.Concrete.Context;
using SignalR.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccess.Concrete.EntityFrameworks
{
    public class EfProductDal : EfRepositoryBase<Product>, IProductDal
    {
        public EfProductDal(SignalRContext context) : base(context)
        {
        }

        public List<Product> GetProductsWithCategories(Expression<Func<Product, object>> include, Expression<Func<Product, bool>> filter = null)
        {
            IQueryable<Product> query = _context.Set<Product>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.Include(include);

            return query.ToList();
        }

        public List<Product> GetProductsWithCategoriesForPaging(int pageNumber, int pageSize, Expression<Func<Product, object>> include, Expression<Func<Product, bool>> filter = null)
        {
            IQueryable<Product> query = _context.Set<Product>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (include != null)
            {
                query = query.Include(include);
            }

            query = query.OrderBy(p => p.ProductID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return query.ToList();
        }
    }
}
