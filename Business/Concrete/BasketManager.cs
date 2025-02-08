using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class BasketManager : IBasketService
    {
        private readonly IBasketDal _basketDal;

        public BasketManager(IBasketDal basketDal)
        {
            _basketDal = basketDal;
        }

        public IDataResult<List<Basket>> TGetAllData(Expression<Func<Basket, bool>> filter = null, Expression<Func<Basket, object>> include = null)
        {
            GeneralErrorException<List<Basket>> generalErrorException = new GeneralErrorException<List<Basket>>();

            return generalErrorException.DataExecuteWithHandling<List<Basket>>(() =>
            {
                List<Basket> values = _basketDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<List<Basket>> TGetBasketByTableNumber(Expression<Func<Basket, object>> include, int id)
        {
            GeneralErrorException<List<Basket>> generalErrorException = new GeneralErrorException<List<Basket>>();

            return generalErrorException.DataExecuteWithHandling<List<Basket>>(() =>
            {
                List<Basket> values = _basketDal.GetBasketByTableNumber(include, id);
                return values;
            });
        }

        public IDataResult<Basket> TGetData(Expression<Func<Basket, bool>> filter = null, Expression<Func<Basket, object>> include = null)
        {
            throw new NotImplementedException();
        }

        public IResult TAddData(Basket entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _basketDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(Basket entity)
        {
            throw new NotImplementedException();
        }

        public IResult TDeleteBasketItem(int id, int tableID)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _basketDal.Queryable().Where(x => x.BasketID == id && x.RestaurantTableID == tableID).ExecuteDelete();
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                _basketDal.DeleteData(id);
                return true;
            });
        }

        public IResult TDeleteBasket(int tableID)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _basketDal.Queryable().Where(x => x.RestaurantTableID == tableID).ExecuteDelete();
                return true;
            });
        }
    }
}
