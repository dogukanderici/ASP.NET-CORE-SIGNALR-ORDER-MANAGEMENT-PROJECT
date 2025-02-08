using SignalR.Dtos.Dtos.BookingDtos;
using SignalRWebUI.Areas.Admin.Dtos.BookingDtos;
using SignalRWebUI.Settings;

namespace SignalRWebUI.Services.Abstract
{
    public interface IBookingServicePS : IDefaultGenericService<ResultBookingDto, List<ResultBookingDto>, CreateAdminBookingDto, UpdateAdminBookingDto>
    {
        Task<IHttpResponseMessageSettings<List<ResultBookingDto>>> GetAllDataByUserAsync(string userMail);
    }
}
