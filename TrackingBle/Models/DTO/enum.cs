namespace TrackingBle.Models.Dto
{
    public enum OrganizationType
    {
        Single,
        Small,
        Medium,
        Big,
        Corporate,
        Government // Note: SQL has "goverment", assuming typo
    }

    public enum ApplicationType
    {
        Empty, // Represents the default empty string in SQL
        Vms,
        Smr,
        Signage,
        Parking,
        Automation,
        Tracking
    }

    public enum LicenseType
    {
        Perpetual,
        Annual
    }

    public enum IntegrationType
    {
        Sdk,
        Api,
        Other
    }

    public enum ApiTypeAuth
    {
        Basic,
        Bearer,
        ApiKey
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public enum StatusEmployee
    {
        Active,
        NonActive,
        Mutation
    }

    // public enum RestrictedStatus
    // {
    //     Restrict,
    //     NonRestrict
    // }

    public enum VisitorStatus
    {
        Waiting,
        Checkin,
        Checkout,
        Denied,
        Block,
        Precheckin,
        Preregist
    }

    public enum AlarmStatus
    {
        NonActive,
        Active
    }
}