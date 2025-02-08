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
    public class RestaurantTableManager : IRestaurantTableService
    {
        private readonly IRestaurantTableDal _restaurantTableDal;

        public RestaurantTableManager(IRestaurantTableDal restaurantTableDal)
        {
            _restaurantTableDal = restaurantTableDal;
        }

        public IDataResult<List<RestaurantTable>> TGetAllData(Expression<Func<RestaurantTable, bool>> filter = null, Expression<Func<RestaurantTable, object>> include = null)
        {
            GeneralErrorException<List<RestaurantTable>> generalErrorException = new GeneralErrorException<List<RestaurantTable>>();

            return generalErrorException.DataExecuteWithHandling<List<RestaurantTable>>(() =>
            {
                List<RestaurantTable> values = _restaurantTableDal.GetAllData(filter, include);

                return values;
            });
        }

        public IDataResult<RestaurantTable> TGetData(Expression<Func<RestaurantTable, bool>> filter = null, Expression<Func<RestaurantTable, object>> include = null)
        {
            GeneralErrorException<RestaurantTable> generalErrorException = new GeneralErrorException<RestaurantTable>();

            return generalErrorException.DataExecuteWithHandling<RestaurantTable>(() =>
            {
                RestaurantTable values = _restaurantTableDal.GetData(filter, include);

                return values;
            });
        }

        public IResult TAddData(RestaurantTable entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _restaurantTableDal.AddData(entity);

                return true;
            });
        }

        public IResult TUpdateData(RestaurantTable entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _restaurantTableDal.UpdateData(entity);

                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _restaurantTableDal.DeleteData(id);

                return state;
            });
        }

        public IDataResult<decimal> TGetActivePassiveRestaurantTableCount(bool status)
        {
            GeneralErrorException<decimal> generalErrorException = new GeneralErrorException<decimal>();

            return generalErrorException.DataExecuteWithHandling<decimal>(() =>
            {
                decimal value = _restaurantTableDal.Queryable().Where(r => r.IsActive == status).Count();

                return value;
            });
        }
    }
}
