namespace SignalRWebUI.Areas.Admin.Dtos.WorkingHourDtos
{
    public class CreateAdminWorkingHourDto
    {
        public string Title { get; set; }
        public string OpeningHour { get; set; }
        public string ClosingHour { get; set; }
        public bool Status { get; set; }
    }
}
