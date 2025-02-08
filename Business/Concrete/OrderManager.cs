using SignalR.Business.Abstract;
using SignalR.Business.Utilities.ErrorExceptions;
using SignalR.Core.Utilities.Result;
using SignalR.DataAccess.Abstract;
using SignalR.DataAccess.Concrete.EntityFrameworks;
using SignalR.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public IDataResult<List<Order>> TGetAllData(Expression<Func<Order, bool>> filter = null, Expression<Func<Order, object>> include = null)
        {
            GeneralErrorException<List<Order>> generalErrorException = new GeneralErrorException<List<Order>>();

            return generalErrorException.DataExecuteWithHandling<List<Order>>(() =>
            {
                List<Order> values = _orderDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<Order> TGetData(Expression<Func<Order, bool>> filter = null, Expression<Func<Order, object>> include = null)
        {
            GeneralErrorException<Order> generalErrorException = new GeneralErrorException<Order>();

            return generalErrorException.DataExecuteWithHandling<Order>(() =>
            {
                Order values = _orderDal.GetData(filter, include);
                return values;
            });
        }

        public IDataResult<Order> TGetDataWithProduct(Expression<Func<Order, bool>> filter = null)
        {
            GeneralErrorException<Order> generalErrorException = new GeneralErrorException<Order>();

            return generalErrorException.DataExecuteWithHandling<Order>(() =>
            {
                Order values = _orderDal.GetDataWithProduct(filter);
                return values;
            });
        }

        public IDataResult<int> TGetActiveOrderCount()
        {
            GeneralErrorException<int> generalErrorException = new GeneralErrorException<int>();

            return generalErrorException.DataExecuteWithHandling<int>(() =>
            {
                int values = _orderDal.Queryable().Where(o => o.OrderStatus == false && o.IsCanceled == false).Count();
                return values;
            });
        }

        public IDataResult<int> TGetCanceledOrderCount()
        {
            GeneralErrorException<int> generalErrorException = new GeneralErrorException<int>();

            return generalErrorException.DataExecuteWithHandling<int>(() =>
            {
                int values = _orderDal.Queryable().Where(o => o.OrderStatus == true && o.IsCanceled == true).Count();
                return values;
            });
        }

        public IDataResult<int> TGetCompletedOrderCount()
        {
            GeneralErrorException<int> generalErrorException = new GeneralErrorException<int>();

            return generalErrorException.DataExecuteWithHandling<int>(() =>
            {
                int values = _orderDal.Queryable().Where(o => o.OrderStatus == true && o.IsCanceled == false).Count();
                return values;
            });
        }

        public IDataResult<decimal> TGetLastOrderPrice()
        {
            GeneralErrorException<decimal> generalErrorException = new GeneralErrorException<decimal>();

            return generalErrorException.DataExecuteWithHandling<decimal>(() =>
            {
                decimal values = _orderDal.Queryable().Where(o => o.IsCanceled != true)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => o.TotalPrice).Take(1).FirstOrDefault();

                return values;
            });
        }

        public IDataResult<int> TGetTotalOrder()
        {
            GeneralErrorException<int> generalErrorException = new GeneralErrorException<int>();

            return generalErrorException.DataExecuteWithHandling<int>(() =>
            {
                int values = _orderDal.Queryable().Count();

                return values;
            });
        }

        public IDataResult<decimal> TGetTodayTotalPrice()
        {
            GeneralErrorException<decimal> generalErrorException = new GeneralErrorException<decimal>();

            return generalErrorException.DataExecuteWithHandling<decimal>(() =>
            {
                decimal value = _orderDal.Queryable().Where(o => o.OrderDate >= (DateTime.Now.Date) && o.OrderDate < (DateTime.Now.AddDays(1).Date)).Sum(o => o.TotalPrice);
                return value;
            });
        }

        public IResult TAddData(Order entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _orderDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(Order entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _orderDal.UpdateData(entity);
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _orderDal.DeleteData(id);

                return state;
            });
        }

        public IDataResult<bool> TDeleteDataByGuid(Guid id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _orderDal.DeleteDataByGuid(id);

                return state;
            });
        }
    }
}
