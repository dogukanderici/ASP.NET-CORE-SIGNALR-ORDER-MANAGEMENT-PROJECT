using SignalR.Business.Abstract;
using SignalR.Business.Settings;
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
    public class MoneyCaseManager : IMoneyCaseService
    {
        private readonly IMoneyCaseDal _moneyCaseDal;

        public MoneyCaseManager(IMoneyCaseDal moneyCaseDal)
        {
            _moneyCaseDal = moneyCaseDal;
        }

        public IDataResult<List<MoneyCase>> TGetAllData(Expression<Func<MoneyCase, bool>> filter = null, Expression<Func<MoneyCase, object>> include = null)
        {
            GeneralErrorException<List<MoneyCase>> generalErrorException = new GeneralErrorException<List<MoneyCase>>();

            return generalErrorException.DataExecuteWithHandling<List<MoneyCase>>(() =>
            {
                List<MoneyCase> values = _moneyCaseDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<MoneyCase> TGetData(Expression<Func<MoneyCase, bool>> filter = null, Expression<Func<MoneyCase, object>> include = null)
        {
            GeneralErrorException<MoneyCase> generalErrorException = new GeneralErrorException<MoneyCase>();

            return generalErrorException.DataExecuteWithHandling<MoneyCase>(() =>
            {
                MoneyCase values = _moneyCaseDal.GetData(filter, include);
                return values;
            });
        }

        public IDataResult<List<PropertySettings>> TGetTotalMoneyCaseAmount()
        {
            GeneralErrorException<List<PropertySettings>> generalErrorException = new GeneralErrorException<List<PropertySettings>>();

            return generalErrorException.DataExecuteWithHandling<List<PropertySettings>>(() =>
            {
                decimal incomeAmount = _moneyCaseDal.Queryable().Where(mc => mc.OperationType == "income" && mc.OperationDate >= DateTime.Now.AddDays(1 - DateTime.Now.Day).Date && mc.OperationDate <= DateTime.Now).Sum(mc => (decimal?)mc.TotalAmount) ?? 0;
                decimal outcomeAmount = _moneyCaseDal.Queryable().Where(mc => mc.OperationType == "outcome" && mc.OperationDate >= DateTime.Now.AddDays(1 - DateTime.Now.Day).Date && mc.OperationDate <= DateTime.Now).Sum(mc => (decimal?)mc.TotalAmount) ?? 0;

                List<PropertySettings> MoneyCaseInfo = new List<PropertySettings>
                    {
                        new PropertySettings {Name="Income",Value=incomeAmount},
                        new PropertySettings {Name="Outcome",Value=outcomeAmount},
                        new PropertySettings {Name="Difference",Value=(incomeAmount-outcomeAmount)},
                    };

                return MoneyCaseInfo;
            });
        }

        public IResult TAddData(MoneyCase entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _moneyCaseDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(MoneyCase entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _moneyCaseDal.UpdateData(entity);
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _moneyCaseDal.DeleteData(id);

                return state;
            });
        }
    }
}
