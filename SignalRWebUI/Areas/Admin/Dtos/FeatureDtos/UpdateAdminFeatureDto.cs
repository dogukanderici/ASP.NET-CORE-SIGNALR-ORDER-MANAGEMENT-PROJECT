namespace SignalRWebUI.Areas.Admin.Dtos.FeatureDtos
{
    public class UpdateAdminFeatureDto
    {
        public int FeatureID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
