namespace TrackingBle.src._15MstIntegration.Models.Dto.MstIntegrationDtos
{
    public class MstIntegrationUpdateDto
    {
        public Guid BrandId { get; set; }
        public string IntegrationType { get; set; }
        public string ApiTypeAuth { get; set; }
        public string ApiUrl { get; set; }
        public string ApiAuthUsername { get; set; }
        public string ApiAuthPasswd { get; set; }
        public string ApiKeyField { get; set; }
        public string ApiKeyValue { get; set; }
        public Guid ApplicationId { get; set; }
    }
}