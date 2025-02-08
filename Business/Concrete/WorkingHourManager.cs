using Microsoft.Data.SqlClient;
using SignalR.Business.Abstract;
using SignalR.Business.Utilities.Constants;
using SignalR.Business.Utilities.ErrorExceptions;
using SignalR.Core.Utilities.Result;
using SignalR.DataAccess.Abstract;
using SignalR.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Concrete
{
    public class WorkingHourManager : IWorkingHourService
    {
        private readonly IWorkingHourDal _workingHourDal;

        public WorkingHourManager(IWorkingHourDal workingHourDal)
        {
            _workingHourDal = workingHourDal;
        }


        public IDataResult<List<WorkingHour>> TGetAllData(Expression<Func<WorkingHour, bool>> filter = null, Expression<Func<WorkingHour, object>> include = null)
        {
            GeneralErrorException<List<WorkingHour>> generalErrorException = new GeneralErrorException<List<WorkingHour>>();

            return generalErrorException.DataExecuteWithHandling<List<WorkingHour>>(() =>
            {
                List<WorkingHour> result = _workingHourDal.GetAllData(filter, include);
                return result;
            });
        }

        public IDataResult<WorkingHour> TGetData(Expression<Func<WorkingHour, bool>> filter = null, Expression<Func<WorkingHour, object>> include = null)
        {
            GeneralErrorException<WorkingHour> generalErrorException = new GeneralErrorException<WorkingHour>();

            return generalErrorException.DataExecuteWithHandling<WorkingHour>(() =>
            {
                WorkingHour result = _workingHourDal.GetData(filter, include);
                return result;
            });
        }

        public IResult TAddData(WorkingHour entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _workingHourDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(WorkingHour entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _workingHourDal.UpdateData(entity);
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _workingHourDal.DeleteData(id);
                return state;
            });
        }
    }
}
