using System.Net;

namespace SignalRWebUI.Settings
{
    public class HttpResponseMessageSettings<TEntity> : IHttpResponseMessageSettings<TEntity>
    {
        public int StatusCode { get; set; }
        public HttpStatusCode StatusMessage { get; set; }
        public string ApiResponseMessage { get; set; }
        public TEntity GetData { get; set; }
    }
}
