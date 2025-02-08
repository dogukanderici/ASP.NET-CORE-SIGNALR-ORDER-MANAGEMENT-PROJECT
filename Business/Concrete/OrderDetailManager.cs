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
    public class OrderDetailManager : IOrderDetailService
    {
        private readonly IOrderDetailDal _orderDetailDal;

        public OrderDetailManager(IOrderDetailDal orderDetailDal)
        {
            _orderDetailDal = orderDetailDal;
        }

        public IDataResult<List<OrderDetail>> TGetAllData(Expression<Func<OrderDetail, bool>> filter = null, Expression<Func<OrderDetail, object>> include = null)
        {
            GeneralErrorException<List<OrderDetail>> generalErrorException = new GeneralErrorException<List<OrderDetail>>();

            return generalErrorException.DataExecuteWithHandling<List<OrderDetail>>(() =>
            {
                List<OrderDetail> values = _orderDetailDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<OrderDetail> TGetData(Expression<Func<OrderDetail, bool>> filter = null, Expression<Func<OrderDetail, object>> include = null)
        {
            GeneralErrorException<OrderDetail> generalErrorException = new GeneralErrorException<OrderDetail>();

            return generalErrorException.DataExecuteWithHandling<OrderDetail>(() =>
            {
                OrderDetail values = _orderDetailDal.GetData(filter, include);
                return values;
            });
        }

        public IResult TAddData(OrderDetail entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _orderDetailDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(OrderDetail entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _orderDetailDal.UpdateData(entity);
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _orderDetailDal.DeleteData(id);

                return state;
            });
        }
    }
}
