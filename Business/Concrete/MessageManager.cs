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
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public IDataResult<List<Message>> TGetAllData(Expression<Func<Message, bool>> filter = null, Expression<Func<Message, object>> include = null)
        {
            GeneralErrorException<List<Message>> generalErrorException = new GeneralErrorException<List<Message>>();

            return generalErrorException.DataExecuteWithHandling<List<Message>>(() =>
            {
                List<Message> values = _messageDal.GetAllData(filter, include);

                return values;
            });
        }

        public IDataResult<Message> TGetData(Expression<Func<Message, bool>> filter = null, Expression<Func<Message, object>> include = null)
        {
            GeneralErrorException<Message> generalErrorException = new GeneralErrorException<Message>();

            return generalErrorException.DataExecuteWithHandling<Message>(() =>
            {
                Message value = _messageDal.GetData(filter, include);

                return value;
            });
        }
        public IResult TAddData(Message entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _messageDal.AddData(entity);

                return true;
            });
        }

        public IResult TUpdateData(Message entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _messageDal.UpdateData(entity);

                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                _messageDal.DeleteData(id);

                return true;
            });
        }
    }
}
