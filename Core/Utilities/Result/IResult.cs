using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Core.Utilities.Result
{
    public interface IResult
    {
        public bool SuccessStatus { get; set; }
        public HttpStatusCode SuccessCode { get; set; }
        public string Message { get; set; }
    }
}
