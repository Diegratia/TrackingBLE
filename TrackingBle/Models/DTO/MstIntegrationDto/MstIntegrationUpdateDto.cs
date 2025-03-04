namespace TrackingBle.Models.Dto.MstIntegrationDto
{
    public class MstIntegrationUpdateDto
    {
        public string BrandId { get; set; }
        public string IntegrationType { get; set; }
        public string ApiTypeAuth { get; set; }
        public string ApiUrl { get; set; }
        public string ApiAuthUsername { get; set; }
        public string ApiAuthPasswd { get; set; }
        public string ApiKeyField { get; set; }
        public string ApiKeyValue { get; set; }
        public Guid ApplicationId { get; set; }
        public long UpdatedBy { get; set; }
        public long UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}