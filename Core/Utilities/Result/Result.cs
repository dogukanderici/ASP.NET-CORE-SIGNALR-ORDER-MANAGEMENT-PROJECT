using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Core.Utilities.Result
{
    public class Result : IResult
    {
        public bool SuccessStatus { get; set; }
        public HttpStatusCode SuccessCode { get; set; }
        public string Message { get; set; }

        public Result(bool successStatus, HttpStatusCode successCode, string message) : this(successStatus, successCode)
        {
            Message = message;
        }

        public Result(bool successStatus, HttpStatusCode successCode)
        {
            SuccessStatus = successStatus;
            SuccessCode = successCode;
        }
    }
}
