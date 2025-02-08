using IdentityModel;
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
    public class HelpDeskManager : IHelpDeskService
    {
        private readonly IHelpDeskDal _helpDeskDal;

        public HelpDeskManager(IHelpDeskDal helpDeskDal)
        {
            _helpDeskDal = helpDeskDal;
        }

        public IDataResult<List<HelpDesk>> TGetAllData(Expression<Func<HelpDesk, bool>> filter = null, Expression<Func<HelpDesk, object>> include = null)
        {
            GeneralErrorException<List<HelpDesk>> generalErrorException = new GeneralErrorException<List<HelpDesk>>();

            return generalErrorException.DataExecuteWithHandling<List<HelpDesk>>(() =>
            {
                List<HelpDesk> values = _helpDeskDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<HelpDesk> TGetData(Expression<Func<HelpDesk, bool>> filter = null, Expression<Func<HelpDesk, object>> include = null)
        {
            GeneralErrorException<HelpDesk> generalErrorException = new GeneralErrorException<HelpDesk>();

            return generalErrorException.DataExecuteWithHandling<HelpDesk>(() =>
            {
                HelpDesk value = _helpDeskDal.GetData(filter, include);
                return value;
            });
        }
        public IResult TAddData(HelpDesk entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _helpDeskDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(HelpDesk entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _helpDeskDal.UpdateData(entity);
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _helpDeskDal.DeleteData(id);
                return state;
            });
        }

        public IDataResult<bool> TDeleteDataGuid(Guid id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _helpDeskDal.DeleteDataByGuid(id);
                return state;
            });
        }
    }
}
