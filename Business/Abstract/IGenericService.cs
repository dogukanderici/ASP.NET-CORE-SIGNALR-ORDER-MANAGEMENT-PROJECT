using SignalR.Core.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Abstract
{
    public interface IGenericService<T>
        where T : class
    {
        //List<T> TGetAllData(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> include = null);
        //T TGetData(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> include = null);

        //void TAddData(T entity);
        //void TUpdateData(T entity);
        //bool TDeleteData(int id);

        IDataResult<List<T>> TGetAllData(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> include = null);
        IDataResult<T> TGetData(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> include = null);

        IResult TAddData(T entity);
        IResult TUpdateData(T entity);
        IDataResult<bool> TDeleteData(int id);
    }
}
