using SignalR.Business.Abstract;
using SignalR.Business.Settings;
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
	public class ProductManager : IProductService
	{
		private readonly IProductDal _productDal;
		private readonly ICategoryDal _categoryDal;

		public ProductManager(IProductDal productDal, ICategoryDal categoryDal)
		{
			_productDal = productDal;
			_categoryDal = categoryDal;
		}

		public IDataResult<List<Product>> TGetAllData(Expression<Func<Product, bool>> filter = null, Expression<Func<Product, object>> include = null)
		{
			GeneralErrorException<List<Product>> generalErrorException = new GeneralErrorException<List<Product>>();

			return generalErrorException.DataExecuteWithHandling<List<Product>>(() =>
			{
				List<Product> values = _productDal.GetAllData(filter, include);
				return values;
			});
		}

		public IDataResult<Product> TGetData(Expression<Func<Product, bool>> filter = null, Expression<Func<Product, object>> include = null)
		{
			GeneralErrorException<Product> generalErrorException = new GeneralErrorException<Product>();

			return generalErrorException.DataExecuteWithHandling<Product>(() =>
			{
				Product values = _productDal.GetData(filter, include);
				return values;
			});
		}

		public IDataResult<decimal> TGetAverageProductPrice()
		{
			GeneralErrorException<decimal> generalErrorException = new GeneralErrorException<decimal>();

			return generalErrorException.DataExecuteWithHandling<decimal>(() =>
			{
				decimal values = _productDal.Queryable().Where(p => p.ProductStatus == true).Average(p => p.Price);
				return values;
			});
		}

		public IDataResult<PropertySettings> TGetMaxProductWithPrice()
		{
			GeneralErrorException<PropertySettings> generalErrorException = new GeneralErrorException<PropertySettings>();

			return generalErrorException.DataExecuteWithHandling<PropertySettings>(() =>
			{
				var value = _productDal.Queryable().Where(p => p.ProductStatus == true && p.Price == (_productDal.Queryable().Max(pp => pp.Price)))
				.Select(p => new { p.ProductID, p.ProductName, p.Price }).FirstOrDefault();

				if (value == null)
					return null;

				return new PropertySettings
				{
					Id = value.ProductID,
					Name = value.ProductName,
					Value = value.Price
				};
			});
		}

		public IDataResult<PropertySettings> TGetMinProductWithPrice()
		{
			GeneralErrorException<PropertySettings> generalErrorException = new GeneralErrorException<PropertySettings>();

			return generalErrorException.DataExecuteWithHandling<PropertySettings>(() =>
			{
				var value = _productDal.Queryable().Where(p => p.ProductStatus == true && p.Price == (_productDal.Queryable().Min(pp => pp.Price)))
				.Select(p => new { p.ProductID, p.ProductName, p.Price }).FirstOrDefault();

				if (value == null)
					return null;
				return new PropertySettings
				{
					Id = value.ProductID,
					Name = value.ProductName,
					Value = value.Price
				};
			});
		}

		public IDataResult<int> TGetProductCount()
		{
			GeneralErrorException<int> generalErrorException = new GeneralErrorException<int>();

			return generalErrorException.DataExecuteWithHandling<int>(() =>
			{
				int values = _productDal.Queryable().Count();
				return values;
			});
		}

		public IDataResult<int> TGetProductCountByCategoryNameDrink()
		{
			GeneralErrorException<int> generalErrorException = new GeneralErrorException<int>();

			return generalErrorException.DataExecuteWithHandling<int>(() =>
			{
				int values = _productDal.Queryable().Where(p => p.CategoryID == (_categoryDal.Queryable()
				.Where(c => c.CategoryName == "İçecekler")
				.Select(i => i.CategoryID)
				.FirstOrDefault())).Count();

				return values;
			});
		}

		public IDataResult<List<Product>> TGetProductsWithCategories(Expression<Func<Product, object>> include, Expression<Func<Product, bool>> filter = null)
		{
			GeneralErrorException<List<Product>> generalErrorException = new GeneralErrorException<List<Product>>();

			return generalErrorException.DataExecuteWithHandling<List<Product>>(() =>
			{
				List<Product> values = _productDal.GetProductsWithCategories(include, filter);
				return values;
			});
		}

		public IDataResult<List<Product>> TGetProductsWithCategoriesByCount(int dataCount, Expression<Func<Product, object>> include, Expression<Func<Product, bool>> filter = null)
		{
			GeneralErrorException<List<Product>> generalErrorException = new GeneralErrorException<List<Product>>();

			return generalErrorException.DataExecuteWithHandling<List<Product>>(() =>
			{
				List<Product> values = _productDal.Queryable().Where(filter).Take(dataCount).ToList();
				return values;
			});
		}

		public IDataResult<List<Product>> TGetProductsWithCategoriesForPaging(int pageNumber, int pageSize, Expression<Func<Product, object>> include, Expression<Func<Product, bool>> filter = null)
		{

			// Toplam Ürün Sayısı
			IDataResult<int> productCount = TGetProductCount();
			int totalProductCount = productCount.Data;

			GeneralErrorException<List<Product>> generalErrorException = new GeneralErrorException<List<Product>>();

			return generalErrorException.DataExecuteWithHandling<List<Product>>(() =>
			{
				List<Product> values = _productDal.GetProductsWithCategoriesForPaging(pageNumber, pageSize, include, filter);
				return values;
			});
		}

		public IResult TAddData(Product entity)
		{
			GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

			return generalErrorException.ResultExecuteWithHandling(() =>
			{
				_productDal.AddData(entity);

				return true;
			});
		}

		public IResult TUpdateData(Product entity)
		{
			GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

			return generalErrorException.ResultExecuteWithHandling(() =>
			{
				_productDal.UpdateData(entity);

				return true;
			});
		}

		public IDataResult<bool> TDeleteData(int id)
		{
			GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

			return generalErrorException.DataExecuteWithHandling<bool>(() =>
			{
				bool state = _productDal.DeleteData(id);

				return state;
			});
		}
	}
}
