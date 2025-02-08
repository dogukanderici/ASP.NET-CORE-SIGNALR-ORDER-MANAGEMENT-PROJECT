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
    public class EfBasketDal : EfRepositoryBase<Basket>, IBasketDal
    {
        public EfBasketDal(SignalRContext context) : base(context)
        {
        }

        public List<Basket> GetBasketByTableNumber(Expression<Func<Basket, object>> include, int id)
        {
            IQueryable<Basket> query = _context.Set<Basket>();

            query = query.Where(x => x.RestaurantTableID == id).Include(include);

            return query.ToList();
        }
    }
}