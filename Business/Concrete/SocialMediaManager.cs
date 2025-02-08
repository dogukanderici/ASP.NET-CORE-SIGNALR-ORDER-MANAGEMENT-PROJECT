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
    public class SocialMediaManager : ISocialMediaService
    {
        private readonly ISocialMediaDal _socialMediaDal;

        public SocialMediaManager(ISocialMediaDal socialMediaDal)
        {
            _socialMediaDal = socialMediaDal;
        }

        public IDataResult<List<SocialMedia>> TGetAllData(Expression<Func<SocialMedia, bool>> filter = null, Expression<Func<SocialMedia, object>> include = null)
        {
            GeneralErrorException<List<SocialMedia>> generalErrorException = new GeneralErrorException<List<SocialMedia>>();

            return generalErrorException.DataExecuteWithHandling<List<SocialMedia>>(() =>
            {
                List<SocialMedia> values = _socialMediaDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<SocialMedia> TGetData(Expression<Func<SocialMedia, bool>> filter = null, Expression<Func<SocialMedia, object>> include = null)
        {
            GeneralErrorException<SocialMedia> generalErrorException = new GeneralErrorException<SocialMedia>();

            return generalErrorException.DataExecuteWithHandling<SocialMedia>(() =>
            {
                SocialMedia values = _socialMediaDal.GetData(filter, include);
                return values;
            });
        }

        public IResult TAddData(SocialMedia entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _socialMediaDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(SocialMedia entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _socialMediaDal.UpdateData(entity);
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _socialMediaDal.DeleteData(id);

                return state;
            });
        }
    }
}
