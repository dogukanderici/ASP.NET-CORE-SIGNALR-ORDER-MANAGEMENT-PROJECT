using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Core.Utilities.Result
{
    public class DataResult<TEntity> : Result, IDataResult<TEntity>
    {
        public TEntity Data { get; set; }

        public DataResult(TEntity data, bool successStatus, HttpStatusCode successCode, string message) : base(successStatus, successCode, message)
        {
            Data = data;
        }

        public DataResult(TEntity data, bool successStatus, HttpStatusCode successCode) : base(successStatus, successCode)
        {
            Data = data;
        }
    }
}
