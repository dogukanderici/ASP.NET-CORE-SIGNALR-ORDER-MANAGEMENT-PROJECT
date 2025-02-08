using System.Net;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class GenericViewModel
    {
        public HttpStatusCode HttpResponseMessage { get; set; }
        public string ApiResponseMessage { get; set; }
    }
}
