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
    public class AboutManager : IAboutService
    {
        private readonly IAboutDal _aboutDal;

        public AboutManager(IAboutDal aboutDal)
        {
            _aboutDal = aboutDal;
        }

        public IDataResult<List<About>> TGetAllData(Expression<Func<About, bool>> filter = null, Expression<Func<About, object>> include = null)
        {
            GeneralErrorException<List<About>> generalErrorException = new GeneralErrorException<List<About>>();

            return generalErrorException.DataExecuteWithHandling<List<About>>(() =>
            {
                List<About> values = _aboutDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<About> TGetData(Expression<Func<About, bool>> filter = null, Expression<Func<About, object>> include = null)
        {
            GeneralErrorException<About> generalErrorException = new GeneralErrorException<About>();

            return generalErrorException.DataExecuteWithHandling<About>(() =>
            {
                About values = _aboutDal.GetData(filter, include);
                return values;
            });
        }

        public IResult TAddData(About entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _aboutDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(About entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _aboutDal.UpdateData(entity);
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _aboutDal.DeleteData(id);
                return state;
            });
        }
    }
}
