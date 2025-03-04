namespace TrackingBle.Models.Dto.MstIntegrationDto
{
    public class MstIntegrationDto
    {
        public int Generate { get; set; }
        public Guid Id { get; set; }
        public string BrandId { get; set; }
        public string IntegrationType { get; set; }
        public string ApiTypeAuth { get; set; }
        public string ApiUrl { get; set; }
        public string ApiAuthUsername { get; set; }
        public string ApiAuthPasswd { get; set; }
        public string ApiKeyField { get; set; }
        public string ApiKeyValue { get; set; }
        public Guid ApplicationId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long UpdatedBy { get; set; }
        public long UpdatedAt { get; set; }
        public int Status { get; set; }
    }
}