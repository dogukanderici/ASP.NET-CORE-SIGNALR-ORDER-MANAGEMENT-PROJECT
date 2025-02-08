using Microsoft.EntityFrameworkCore;
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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>> TGetAllData(Expression<Func<Category, bool>> filter = null, Expression<Func<Category, object>> include = null)
        {
            GeneralErrorException<List<Category>> generalErrorException = new GeneralErrorException<List<Category>>();

            return generalErrorException.DataExecuteWithHandling<List<Category>>(() =>
            {
                List<Category> values = _categoryDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<Category> TGetData(Expression<Func<Category, bool>> filter = null, Expression<Func<Category, object>> include = null)
        {
            GeneralErrorException<Category> generalErrorException = new GeneralErrorException<Category>();

            return generalErrorException.DataExecuteWithHandling<Category>(() =>
            {
                Category values = _categoryDal.GetData(filter, include);
                return values;
            });
        }

        public IDataResult<int> GetCategoryActiveCount()
        {
            GeneralErrorException<int> generalErrorException = new GeneralErrorException<int>();

            return generalErrorException.DataExecuteWithHandling<int>(() =>
            {
                int values = _categoryDal.Queryable().Where(c => c.CategoryStatus == true).Count();
                return values;
            });
        }

        public IResult TAddData(Category entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _categoryDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(Category entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _categoryDal.UpdateData(entity);
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _categoryDal.DeleteData(id);

                return state;
            });
        }
    }
}
