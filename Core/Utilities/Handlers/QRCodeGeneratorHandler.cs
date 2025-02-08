using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Core.Utilities.Handlers
{
    public class QRCodeGeneratorHandler : IQRCodeGeneratorHandler
    {
        public string CreateQRCode(string value, int graphicLevel)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                QRCodeGenerator createQRCode = new QRCodeGenerator();

                QRCodeGenerator.QRCode squareCode = createQRCode.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);

                using (Bitmap image = squareCode.GetGraphic(graphicLevel))
                {
                    image.Save(memoryStream, ImageFormat.Png);

                    return "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }
}
