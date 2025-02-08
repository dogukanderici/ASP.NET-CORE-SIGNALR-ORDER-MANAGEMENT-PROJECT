namespace SignalRWebUI.Areas.Admin.Dtos.MoneyCaseDtos
{
    public class ResultAdminMoneyCaseDto
    {
        public int MoneyCaseID { get; set; }
        public decimal TotalAmount { get; set; }
        public string OperationType { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
