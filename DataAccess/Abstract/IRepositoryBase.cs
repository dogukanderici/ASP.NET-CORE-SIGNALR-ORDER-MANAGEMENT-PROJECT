using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccess.Abstract
{
    public interface IRepositoryBase<T>
        where T : class, new()
    {
        List<T> GetAllData(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> includes = null);
        T GetData(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> includes = null);

        IQueryable<T> Queryable();

        void AddData(T entity);
        void UpdateData(T entity);
        bool DeleteData(int id);
        bool DeleteDataByGuid(Guid id);
    }
}
