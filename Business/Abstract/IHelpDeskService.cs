using SignalR.Core.Utilities.Result;
using SignalR.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Business.Abstract
{
    public interface IHelpDeskService : IGenericService<HelpDesk>
    {
        IDataResult<bool> TDeleteDataGuid(Guid id);
    }
}
