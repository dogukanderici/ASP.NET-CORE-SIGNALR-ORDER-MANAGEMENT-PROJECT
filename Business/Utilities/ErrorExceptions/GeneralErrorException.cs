using Microsoft.Data.SqlClient;
using SignalR.Business.Utilities.Constants;
using SignalR.Core.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.ErrorExceptions
{
    public class GeneralErrorException<TEntity> : IGeneralErrorException<TEntity>
    {
        public IDataResult<TEntity> DataExecuteWithHandling<TEntity>(Func<TEntity> func)
        {
            try
            {
                var result = func();
                return new SuccessDataResult<TEntity>(result, HttpStatusCode.OK, $"{ServiceMessageContants.ProcessSuccess}");
            }
            catch (NullReferenceException ex)
            {
                return new ErrorDataResult<TEntity>(default, HttpStatusCode.InternalServerError, $"{ServiceMessageContants.ProcessFailedWithNull} Error: {ex.Message}");
            }
            catch (SqlException ex)
            {
                return new ErrorDataResult<TEntity>(default, HttpStatusCode.ServiceUnavailable, $"{ServiceMessageContants.ProcessFailedWithSQL} Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TEntity>(default, HttpStatusCode.BadRequest, $"{ServiceMessageContants.ProcessFailedWithSystem}");
            }
        }

        public IResult ResultExecuteWithHandling(Func<bool> func)
        {
            try
            {
                var result = func();
                return new SuccessResult(HttpStatusCode.OK, $"{ServiceMessageContants.ProcessSuccess}");
            }
            catch (NullReferenceException ex)
            {
                return new ErrorResult(HttpStatusCode.InternalServerError, $"{ServiceMessageContants.ProcessFailedWithNull} Error: {ex.Message}");
            }
            catch (SqlException ex)
            {
                return new ErrorResult(HttpStatusCode.ServiceUnavailable, $"{ServiceMessageContants.ProcessFailedWithSQL} Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return new ErrorResult(HttpStatusCode.BadRequest, $"{ServiceMessageContants.ProcessFailedWithSystem}");
            }
        }
    }
}
