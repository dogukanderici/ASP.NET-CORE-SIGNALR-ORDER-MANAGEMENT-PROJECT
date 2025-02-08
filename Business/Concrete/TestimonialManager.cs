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
    public class TestimonialManager : ITestimonialService
    {
        private readonly ITestimonialDal _testimonialDal;

        public TestimonialManager(ITestimonialDal testimonialDal)
        {
            _testimonialDal = testimonialDal;
        }

        public IDataResult<List<Testimonial>> TGetAllData(Expression<Func<Testimonial, bool>> filter = null, Expression<Func<Testimonial, object>> include = null)
        {
            GeneralErrorException<List<Testimonial>> generalErrorException = new GeneralErrorException<List<Testimonial>>();

            return generalErrorException.DataExecuteWithHandling<List<Testimonial>>(() =>
            {
                List<Testimonial> values = _testimonialDal.GetAllData(filter, include);
                return values;
            });
        }

        public IDataResult<Testimonial> TGetData(Expression<Func<Testimonial, bool>> filter = null, Expression<Func<Testimonial, object>> include = null)
        {
            GeneralErrorException<Testimonial> generalErrorException = new GeneralErrorException<Testimonial>();

            return generalErrorException.DataExecuteWithHandling<Testimonial>(() =>
            {
                Testimonial values = _testimonialDal.GetData(filter, include);
                return values;
            });
        }

        public IResult TAddData(Testimonial entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _testimonialDal.AddData(entity);
                return true;
            });
        }

        public IResult TUpdateData(Testimonial entity)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.ResultExecuteWithHandling(() =>
            {
                _testimonialDal.UpdateData(entity);
                return true;
            });
        }

        public IDataResult<bool> TDeleteData(int id)
        {
            GeneralErrorException<bool> generalErrorException = new GeneralErrorException<bool>();

            return generalErrorException.DataExecuteWithHandling<bool>(() =>
            {
                bool state = _testimonialDal.DeleteData(id);

                return state;
            });
        }
    }
}
