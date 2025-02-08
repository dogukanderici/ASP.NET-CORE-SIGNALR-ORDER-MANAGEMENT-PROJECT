using SignalR.Core.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Utilities.ErrorExceptions
{
    public interface IGeneralErrorException<T>
    {
        public IDataResult<T> DataExecuteWithHandling<T>(Func<T> func);
        public IResult ResultExecuteWithHandling(Func<bool> func);
    }
}
