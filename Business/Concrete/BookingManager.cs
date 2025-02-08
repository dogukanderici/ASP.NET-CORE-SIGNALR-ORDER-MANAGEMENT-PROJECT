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
    public class BookingManager : IBookingService
    {
        private readonly IBookingDal _bookingDal;

        public BookingManager(IBookingDal bookingDal)
        {
            _bookingDal = bookingDal;
        }

        public IDataResult<List<Booking>> TGetAllData(Expression<Func<Booking, bool>> filter = null, Expression<Func<Booking, object>> include = null)
        {
            GeneralErrorException<List<Booking>> generalErrorException = new GeneralErrorException<List<Booking>>();

            return generalErrorException.DataExecuteWithHandling<List<Booking>>(() =>
            {
                List<Booking> values = _bookingDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<Booking> TGetData(Expression<Func<Booking, bool>> filter = null, Expression<Func<Booking, object>> include = null)
        {
            GeneralErrorException<Booking> generalErrorException = new GeneralErrorException<Booking>();

            return generalErrorException.DataExecuteWithHandling<Booking>(() =>
            {
                Booking values = _bookingDal.GetData(filter, include);
                return values;
            });
        }

        public IResult TAddData(Booking entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _bookingDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(Booking entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _bookingDal.UpdateData(entity);
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _bookingDal.DeleteData(id);

                return state;
            });
        }
    }
}
