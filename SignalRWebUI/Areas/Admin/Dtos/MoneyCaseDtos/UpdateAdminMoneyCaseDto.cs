namespace SignalRWebUI.Areas.Admin.Dtos.MoneyCaseDtos
{
    public class UpdateAdminMoneyCaseDto
    {
        public int MoneyCaseID { get; set; }
        public decimal TotalAmount { get; set; }
        public string OperationType { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
