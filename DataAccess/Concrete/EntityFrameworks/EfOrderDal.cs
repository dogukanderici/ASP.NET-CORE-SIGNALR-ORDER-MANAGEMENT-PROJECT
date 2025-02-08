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
    public class EfOrderDal : EfRepositoryBase<Order>, IOrderDal
    {
        public EfOrderDal(SignalRContext context) : base(context)
        {
        }

        public Order GetDataWithProduct(Expression<Func<Order, bool>> filter = null)
        {
            IQueryable<Order> query = _context.Set<Order>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.Include(x => x.OrderDetails).ThenInclude(od => od.Product);

            return query.FirstOrDefault();
        }
    }
}
