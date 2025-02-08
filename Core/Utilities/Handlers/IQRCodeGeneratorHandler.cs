using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Core.Utilities.Handlers
{
    public interface IQRCodeGeneratorHandler
    {
        string CreateQRCode(string value, int graphicLevel);
    }
}
