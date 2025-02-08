using SignalR.Core.DataAccess.Concrete.EntityFramework;
using SignalR.DataAccess.Abstract;
using SignalR.DataAccess.Concrete.Context;
using SignalR.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DataAccess.Concrete.EntityFrameworks
{
    public class EfMoneyCaseDal : EfRepositoryBase<MoneyCase>, IMoneyCaseDal
    {
        public EfMoneyCaseDal(SignalRContext context) : base(context)
        {
        }
    }
}
