using SignalR.Core.Utilities.Result;
using SignalR.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Abstract
{
    public interface IDiscountService : IGenericService<Discount>
    {
        IDataResult<bool> TDeleteDataByGuid(Guid id);
    }
}
