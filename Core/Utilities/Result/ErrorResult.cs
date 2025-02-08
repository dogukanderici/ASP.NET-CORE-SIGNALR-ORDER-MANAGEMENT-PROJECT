using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Core.Utilities.Result
{
    public class ErrorResult : Result
    {
        public ErrorResult(HttpStatusCode successCode, string message) : base(false, successCode, message)
        {

        }

        public ErrorResult(HttpStatusCode successCode) : base(false, successCode)
        {

        }
    }
}
