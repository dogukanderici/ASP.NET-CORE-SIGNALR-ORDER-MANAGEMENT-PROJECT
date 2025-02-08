using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Core.Utilities.Result
{
    public class SuccessDataResult<TEntity> : DataResult<TEntity>
    {
        public SuccessDataResult(TEntity data, HttpStatusCode successCode, string message) : base(data, true, successCode, message)
        {

        }
        public SuccessDataResult(TEntity data, HttpStatusCode successCode) : base(data, true, successCode)
        {

        }

        public SuccessDataResult(HttpStatusCode successCode) : base(default, true, successCode)
        {

        }
    }
}
