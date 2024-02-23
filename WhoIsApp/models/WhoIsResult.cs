namespace WhoIsApp.models
{
    public class WhoIsResult
    {
        public DomainEntity Domain{get;set;}
        public MainInfo Registrant { get; set; }

        public WhoIsResult()
        {
            Domain = new DomainEntity();
            Registrant = new MainInfo();
        }
    }

    public class DomainEntity
    {
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdateDate { get; set; }
        public string ExpireDate { get; set; }

        public DomainEntity()
        {
            Name = string.Empty;
            CreatedDate = string.Empty;
            LastUpdateDate = string.Empty;
            ExpireDate = string.Empty;
        }
    }

    public class MainInfo
    {
        public string Name { get; set; }
        public Organization Organization { get; set; }
        public Address Address { get; set; }
        public string Email { get; internal set; }

        public MainInfo()
        {
            Name = string.Empty;
            Email = string.Empty;
            Organization = new Organization();
            Address = new Address();
        }
    }

    public class Organization
    {
        public string Name { get; set; }

        public Organization()
        {
            Name = string.Empty;
        }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }

        public Address()
        {
            Street = string.Empty;
            City = string.Empty;
            PostalCode = string.Empty;
            CountryCode = string.Empty;
        }
    }
}
