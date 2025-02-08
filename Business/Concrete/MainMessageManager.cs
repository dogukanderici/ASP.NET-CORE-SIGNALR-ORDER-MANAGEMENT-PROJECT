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
    public class MainMessageManager : IMainMessageService
    {
        private readonly IMainMessageDal _mainMessageDal;

        public MainMessageManager(IMainMessageDal mainMessageDal)
        {
            _mainMessageDal = mainMessageDal;
        }

        public IDataResult<List<MainMessage>> TGetAllData(Expression<Func<MainMessage, bool>> filter = null, Expression<Func<MainMessage, object>> include = null)
        {
            GeneralErrorException<List<MainMessage>> generalErrorException = new GeneralErrorException<List<MainMessage>>();

            return generalErrorException.DataExecuteWithHandling<List<MainMessage>>(() =>
            {
                List<MainMessage> values = _mainMessageDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<MainMessage> TGetData(Expression<Func<MainMessage, bool>> filter = null, Expression<Func<MainMessage, object>> include = null)
        {
            GeneralErrorException<MainMessage> generalErrorException = new GeneralErrorException<MainMessage>();

            return generalErrorException.DataExecuteWithHandling<MainMessage>(() =>
            {
                MainMessage values = _mainMessageDal.GetData(filter, include);
                return values;
            });
        }

        public IResult TAddData(MainMessage entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _mainMessageDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(MainMessage entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _mainMessageDal.UpdateData(entity);
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                _mainMessageDal.DeleteData(id);
                return true;
            });
        }
    }
}
