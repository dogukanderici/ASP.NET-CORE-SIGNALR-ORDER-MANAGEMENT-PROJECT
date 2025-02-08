using SignalR.Core.Utilities.Result;
using SignalR.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Abstract
{
    public interface IOrderService : IGenericService<Order>
    {
        IDataResult<bool> TDeleteDataByGuid(Guid id);
        IDataResult<int> TGetTotalOrder();
        IDataResult<int> TGetActiveOrderCount();
        IDataResult<int> TGetCompletedOrderCount();
        IDataResult<int> TGetCanceledOrderCount();
        IDataResult<decimal> TGetLastOrderPrice();
        IDataResult<decimal> TGetTodayTotalPrice();
        IDataResult<Order> TGetDataWithProduct(Expression<Func<Order, bool>> filter = null);
    }
}
