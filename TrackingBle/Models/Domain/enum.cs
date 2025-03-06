namespace TrackingBle.Models.Domain
{
    public enum OrganizationType
    {
        Single,
        Small,
        Medium,
        Big,
        Corporate,
        Government
    }

    public enum ApplicationType
    {
        Empty, // default
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

    public enum RestrictedStatus
    {   
        Restrict,
        NonRestrict
    }

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