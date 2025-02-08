namespace SignalRWebUI.Areas.Admin.Dtos.MoneyCaseDtos
{
    public class CreateAdminMoneyCaseDto
    {
        public decimal TotalAmount { get; set; }
        public string OperationType { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
