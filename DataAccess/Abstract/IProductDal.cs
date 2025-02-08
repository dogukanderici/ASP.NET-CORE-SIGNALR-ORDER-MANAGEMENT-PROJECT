using SignalR.Entities.Concrete;
using System.Linq.Expressions;

namespace SignalR.DataAccess.Abstract
{
    public interface IProductDal : IRepositoryBase<Product>
    {
        List<Product> GetProductsWithCategories(Expression<Func<Product, object>> include, Expression<Func<Product, bool>> filter = null);
        List<Product> GetProductsWithCategoriesForPaging(int pageNumber, int pageSize, Expression<Func<Product, object>> include, Expression<Func<Product, bool>> filter = null);
    }
}
