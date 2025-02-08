using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Core.Utilities.Result
{
    public class SuccessResult : Result
    {
        public SuccessResult(HttpStatusCode successCode, string message) : base(true, successCode, message)
        {

        }

        public SuccessResult(HttpStatusCode successCode) : base(true, successCode)
        {

        }
    }
}
