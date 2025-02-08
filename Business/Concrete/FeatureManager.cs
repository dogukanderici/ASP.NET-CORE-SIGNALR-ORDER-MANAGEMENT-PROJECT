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
    public class FeatureManager : IFeatureService
    {
        private readonly IFeatureDal _featureDal;

        public FeatureManager(IFeatureDal featureDal)
        {
            _featureDal = featureDal;
        }

        public IDataResult<List<Feature>> TGetAllData(Expression<Func<Feature, bool>> filter = null, Expression<Func<Feature, object>> include = null)
        {
            GeneralErrorException<List<Feature>> generalErrorException = new GeneralErrorException<List<Feature>>();

            return generalErrorException.DataExecuteWithHandling<List<Feature>>(() =>
            {
                List<Feature> values = _featureDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<Feature> TGetData(Expression<Func<Feature, bool>> filter = null, Expression<Func<Feature, object>> include = null)
        {
            GeneralErrorException<Feature> generalErrorException = new GeneralErrorException<Feature>();

            return generalErrorException.DataExecuteWithHandling<Feature>(() =>
            {
                Feature values = _featureDal.GetData(filter, include);
                return values;
            });
        }

        public IResult TAddData(Feature entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _featureDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(Feature entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _featureDal.UpdateData(entity);
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _featureDal.DeleteData(id);

                return state;
            });
        }
    }
}
