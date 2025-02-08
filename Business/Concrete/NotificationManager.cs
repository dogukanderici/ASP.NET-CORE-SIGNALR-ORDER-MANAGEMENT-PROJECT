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
    public class NotificationManager : INotificationService
    {
        private readonly INotificationDal _notificationDal;

        public NotificationManager(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }

        public IDataResult<List<Notification>> TGetAllData(Expression<Func<Notification, bool>> filter = null, Expression<Func<Notification, object>> include = null)
        {
            GeneralErrorException<List<Notification>> generalErrorException = new GeneralErrorException<List<Notification>>();

            return generalErrorException.DataExecuteWithHandling<List<Notification>>(() =>
            {
                List<Notification> values = _notificationDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<Notification> TGetData(Expression<Func<Notification, bool>> filter = null, Expression<Func<Notification, object>> include = null)
        {
            GeneralErrorException<Notification> generalErrorException = new GeneralErrorException<Notification>();

            return generalErrorException.DataExecuteWithHandling<Notification>(() =>
            {
                Notification value = _notificationDal.GetData(filter, include);

                return value;
            });
        }

        public IDataResult<int> GetUnreadNotificationCount()
        {
            GeneralErrorException<int> generalErrorException = new GeneralErrorException<int>();

            return generalErrorException.DataExecuteWithHandling<int>(() =>
            {
                int value = _notificationDal.Queryable().Where(n => n.Status == false).Count();

                return value;
            });
        }

        public IResult TAddData(Notification entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _notificationDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(Notification entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _notificationDal.UpdateData(entity);
                return true;
            });
        }

        public IResult TUpdateNotificationStatus(int id, bool status)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                Notification value = _notificationDal.GetData(n => n.NotificationID == id, null);

                if (value != null)
                {
                    value.Status = status;
                    TUpdateData(value);

                    return true;
                }

                return false;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                _notificationDal.DeleteData(id);
                return true;
            });
        }
    }
}
