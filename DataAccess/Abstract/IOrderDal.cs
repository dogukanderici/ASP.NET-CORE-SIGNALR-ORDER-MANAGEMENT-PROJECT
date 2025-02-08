using SignalR.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccess.Abstract
{
    public interface IOrderDal : IRepositoryBase<Order>
    {
        Order GetDataWithProduct(Expression<Func<Order, bool>> filter = null);
    }
}
