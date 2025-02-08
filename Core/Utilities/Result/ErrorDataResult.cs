using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Core.Utilities.Result
{
    public class ErrorDataResult<TEntity> : DataResult<TEntity>
    {
        public ErrorDataResult(TEntity data, HttpStatusCode successCode, string message) : base(data, false, successCode, message)
        {

        }

        public ErrorDataResult(TEntity data, HttpStatusCode successCode) : base(data, false, successCode)
        {

        }
        public ErrorDataResult(HttpStatusCode successCode) : base(default, false, successCode)
        {

        }
    }
}
