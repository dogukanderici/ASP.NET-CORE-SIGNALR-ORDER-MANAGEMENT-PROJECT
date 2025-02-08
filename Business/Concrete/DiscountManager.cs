using SignalR.Business.Abstract;
using SignalR.Business.Utilities.ErrorExceptions;
using SignalR.Core.Utilities.Result;
using SignalR.DataAccess.Abstract;
using SignalR.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Concrete
{
    public class DiscountManager : IDiscountService
    {
        private readonly IDiscountDal _discountDal;

        public DiscountManager(IDiscountDal discountDal)
        {
            _discountDal = discountDal;
        }

        public IDataResult<List<Discount>> TGetAllData(Expression<Func<Discount, bool>> filter = null, Expression<Func<Discount, object>> include = null)
        {
            GeneralErrorException<List<Discount>> generalErrorException = new GeneralErrorException<List<Discount>>();

            return generalErrorException.DataExecuteWithHandling<List<Discount>>(() =>
            {
                return _discountDal.GetAllData(filter, include);
            });
        }

        public IDataResult<Discount> TGetData(Expression<Func<Discount, bool>> filter = null, Expression<Func<Discount, object>> include = null)
        {
            GeneralErrorException<Discount> generalErrorException = new GeneralErrorException<Discount>();

            return generalErrorException.DataExecuteWithHandling<Discount>(() =>
            {
                return _discountDal.GetData(filter, include);
            });
        }
        public IResult TAddData(Discount entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _discountDal.AddData(entity);

                return true;
            });
        }

        public IResult TUpdateData(Discount entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _discountDal.UpdateData(entity);

                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                return _discountDal.DeleteData(id);
            });
        }

        public IDataResult<bool> TDeleteDataByGuid(Guid id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                return _discountDal.DeleteDataByGuid(id);
            });
        }
    }
}
