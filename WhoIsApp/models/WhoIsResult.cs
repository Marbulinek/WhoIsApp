namespace WhoIsApp.models
{
    public class WhoIsResult
    {
        public DomainEntity Domain{get;set;} = new();
        public MainInfo Registrant { get; set; } = new();
    }

    public class DomainEntity
    {
        public string Name { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public string LastUpdateDate { get; set; } = string.Empty;
        public string ExpireDate { get; set; } = string.Empty;
    }

    public class MainInfo
    {
        public string Name { get; set; } = string.Empty;
        public Organization Organization { get; set; } = new();
        public Address Address { get; set; } = new();
        public string Email { get; internal set; } = string.Empty;
    }

    public class Organization
    {
        public string Name { get; set; } = string.Empty;
    }

    public class Address
    {
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
    }
}
