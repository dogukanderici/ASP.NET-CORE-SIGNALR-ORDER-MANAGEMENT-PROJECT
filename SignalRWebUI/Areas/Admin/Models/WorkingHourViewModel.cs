using SignalR.Dtos.Dtos.WorkingHourDtos;
using SignalRWebUI.Areas.Admin.Dtos.WorkingHourDtos;

namespace SignalRWebUI.Areas.Admin.Models
{
    public class WorkingHourViewModel : GenericViewModel
    {
        public List<ResultWorkingHourDto> ResultWorkingHours { get; set; }
        public CreateAdminWorkingHourDto CreateWorkingHour { get; set; }
        public UpdateAdminWorkingHourDto UpdateWorkingHour { get; set; }
    }
}
