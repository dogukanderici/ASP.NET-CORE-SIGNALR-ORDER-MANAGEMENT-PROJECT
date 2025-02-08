using SignalR.Dtos.Dtos.AboutDtos;
using System.Net;

namespace SignalRWebUI.Models
{
    public class DefaultAboutViewModel
    {
        public List<ResultAboutDto> ResultAbouts { get; set; }

        public string HttpResponseMessage { get; set; }
    }
}
