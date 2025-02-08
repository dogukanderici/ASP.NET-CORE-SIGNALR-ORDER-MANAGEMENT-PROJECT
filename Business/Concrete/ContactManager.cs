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
    public class ContactManager : IContactService
    {
        private readonly IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public IDataResult<List<Contact>> TGetAllData(Expression<Func<Contact, bool>> filter = null, Expression<Func<Contact, object>> include = null)
        {
            GeneralErrorException<List<Contact>> generalErrorException = new GeneralErrorException<List<Contact>>();

            return generalErrorException.DataExecuteWithHandling<List<Contact>>(() =>
            {
                List<Contact> values = _contactDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<Contact> TGetData(Expression<Func<Contact, bool>> filter = null, Expression<Func<Contact, object>> include = null)
        {
            GeneralErrorException<Contact> generalErrorException = new GeneralErrorException<Contact>();

            return generalErrorException.DataExecuteWithHandling<Contact>(() =>
            {
                Contact value = _contactDal.GetData(filter, include);
                return value;
            });
        }

        public IResult TAddData(Contact entity)
        {
            GeneralErrorException<Contact> generalErrorException = new GeneralErrorException<Contact>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _contactDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(Contact entity)
        {
            GeneralErrorException<Contact> generalErrorException = new GeneralErrorException<Contact>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _contactDal.UpdateData(entity);
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _contactDal.DeleteData(id);

                return state;
            });
        }
    }
}
