using TrackingBle.src._15MstIntegration.Models.Domain;

namespace TrackingBle.src._15MstIntegration.Models.Dto.MstIntegrationDtos
{
    public class MstIntegrationDto
    {
        public int Generate { get; set; }
        public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public string IntegrationType { get; set; }
        public string ApiTypeAuth { get; set; }
        public string ApiUrl { get; set; }
        public string ApiAuthUsername { get; set; }
        public string ApiAuthPasswd { get; set; }
        public string ApiKeyField { get; set; }
        public string ApiKeyValue { get; set; }
        public Guid ApplicationId { get; set; } // Tetap ada sebagai Guid
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? Status { get; set; }
        public MstBrandDto Brand { get; set; } // Hanya Brand yang disertakan
    }

    public class MstBrandDto
    {
        public int Generate { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public int? Status { get; set; }
    }

}