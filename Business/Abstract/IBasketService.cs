using SignalR.Core.Utilities.Result;
using SignalR.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Abstract
{
    public interface IBasketService : IGenericService<Basket>
    {
        IDataResult<List<Basket>> TGetBasketByTableNumber(Expression<Func<Basket, object>> include, int id);
        IResult TDeleteBasket(int tableID);
        IResult TDeleteBasketItem(int id, int tableID);
    }
}
