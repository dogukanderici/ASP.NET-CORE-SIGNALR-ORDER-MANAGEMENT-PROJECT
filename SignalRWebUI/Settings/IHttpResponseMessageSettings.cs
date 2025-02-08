using System.Net;

namespace SignalRWebUI.Settings
{
    public interface IHttpResponseMessageSettings<T>
    {
        public int StatusCode { get; set; }
        public HttpStatusCode StatusMessage { get; set; }
        public string ApiResponseMessage { get; set; }
        public T GetData { get; set; }
    }
}
