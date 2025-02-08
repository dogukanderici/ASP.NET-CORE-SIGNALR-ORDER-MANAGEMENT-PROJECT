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
    public class DailyDiscountManager : IDailyDiscountService
    {
        private readonly IDailyDiscountDal _dailyDiscountDal;

        public DailyDiscountManager(IDailyDiscountDal dailyDiscountDal)
        {
            _dailyDiscountDal = dailyDiscountDal;
        }

        public IDataResult<List<DailyDiscount>> TGetAllData(Expression<Func<DailyDiscount, bool>> filter = null, Expression<Func<DailyDiscount, object>> include = null)
        {
            GeneralErrorException<List<DailyDiscount>> generalErrorException = new GeneralErrorException<List<DailyDiscount>>();

            return generalErrorException.DataExecuteWithHandling<List<DailyDiscount>>(() =>
            {
                List<DailyDiscount> values = _dailyDiscountDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<DailyDiscount> TGetData(Expression<Func<DailyDiscount, bool>> filter = null, Expression<Func<DailyDiscount, object>> include = null)
        {
            GeneralErrorException<DailyDiscount> generalErrorException = new GeneralErrorException<DailyDiscount>();

            return generalErrorException.DataExecuteWithHandling<DailyDiscount>(() =>
            {
                DailyDiscount values = _dailyDiscountDal.GetData(filter, include);
                return values;
            });
        }

        public IResult TAddData(DailyDiscount entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _dailyDiscountDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(DailyDiscount entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _dailyDiscountDal.UpdateData(entity);
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _dailyDiscountDal.DeleteData(id);

                return state;
            });
        }
    }
}
