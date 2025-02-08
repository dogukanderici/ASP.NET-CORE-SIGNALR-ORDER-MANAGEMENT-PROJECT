using SignalR.Business.Settings;
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
	public interface IProductService : IGenericService<Product>
	{
		IDataResult<List<Product>> TGetProductsWithCategories(Expression<Func<Product, object>> include, Expression<Func<Product, bool>> filter = null);
		IDataResult<List<Product>> TGetProductsWithCategoriesByCount(int dataCount, Expression<Func<Product, object>> include, Expression<Func<Product, bool>> filter = null);
		IDataResult<List<Product>> TGetProductsWithCategoriesForPaging(int pageNumber, int pageSize, Expression<Func<Product, object>> include, Expression<Func<Product, bool>> filter = null);
		IDataResult<int> TGetProductCount();
		IDataResult<int> TGetProductCountByCategoryNameDrink();
		IDataResult<decimal> TGetAverageProductPrice();
		IDataResult<PropertySettings> TGetMaxProductWithPrice();
		IDataResult<PropertySettings> TGetMinProductWithPrice();
	}
}
