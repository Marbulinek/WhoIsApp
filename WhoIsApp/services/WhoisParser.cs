using System.Text.RegularExpressions;
using WhoIsApp.models;

namespace WhoIsApp.services
{
    public class WhoisParser
    {
        private readonly ModelManager _modelManager = new();

        public WhoIsResult Parse(string whoisRawText)
        {
            var whoisResult = new WhoIsResult();

            // prepare model
            _modelManager.PrepareModel(whoisRawText);

            // dates
            string[] parametersDomainName = ["domain", "domain name"];
            whoisResult.Domain.Name = _modelManager.SearchModel(parametersDomainName);

            string[] parametersCreatedDate = ["creation date", "created", "registered"];
            whoisResult.Domain.CreatedDate = _modelManager.SearchModel(parametersCreatedDate).FormatDateTime();

            string[] parametersUpdateDate = ["updated date", "updated", "changed", "last update"];
            whoisResult.Domain.LastUpdateDate = _modelManager.SearchModel(parametersUpdateDate).FormatDateTime();

            string[] parametersExpiryDate = ["valid until", "expire", "expire date", "expiration date"];
            whoisResult.Domain.ExpireDate = _modelManager.SearchModel(parametersExpiryDate).FormatDateTime();
            
            // registrant
            string[] parametersRegistrantName = ["name", "registrant name"];
            whoisResult.Registrant.Name = _modelManager.SearchModel(parametersRegistrantName);

            string[] parametersRegistrantEmail = ["email", "mail"];
            whoisResult.Registrant.Email = _modelManager.SearchModel(parametersRegistrantEmail);

            string[] parametersRegistrantOrganization = ["organization", "org", "registrant organization"];
            whoisResult.Registrant.Organization.Name = _modelManager.SearchModel(parametersRegistrantOrganization);

            // address
            if (IsFullAddress(whoisRawText))
            {
                // registar organization address
                string[] parametersRegistrantAddressStreet = ["street", "registrant street"];
                whoisResult.Registrant.Address.Street = _modelManager.SearchModel(parametersRegistrantAddressStreet);

                string[] parametersRegistrantAddressCity = ["city", "registrant city"];
                whoisResult.Registrant.Address.City = _modelManager.SearchModel(parametersRegistrantAddressCity);

                string[] parametersRegistrantAddressPostal = ["postal code", "registrant postal code"];
                whoisResult.Registrant.Address.PostalCode = _modelManager.SearchModel(parametersRegistrantAddressPostal);

                string[] parametersRegistrantAddressCountryCode = ["country code", "country", "registrant country"];
                whoisResult.Registrant.Address.CountryCode = _modelManager.SearchModel(parametersRegistrantAddressCountryCode);
            }
            else if (IsOnlySimpleAddress(whoisRawText))
            {
                string[] parametersRegistrantAddress = ["address", "registrant address"];
                whoisResult.Registrant.Address.Street = _modelManager.SearchModel(parametersRegistrantAddress);
                whoisResult.Registrant.Address.City = _modelManager.SearchModel(parametersRegistrantAddress);
                whoisResult.Registrant.Address.PostalCode = _modelManager.SearchModel(parametersRegistrantAddress);
                whoisResult.Registrant.Address.CountryCode = _modelManager.SearchModel(parametersRegistrantAddress);
            }

            return whoisResult;
        }

        private static bool IsFullAddress(string whoisRawText)
        {
            return Regex.IsMatch(whoisRawText, @"\bstreet\b", RegexOptions.IgnoreCase) ||
                   Regex.IsMatch(whoisRawText, @"\bcity\b", RegexOptions.IgnoreCase) ||
                   Regex.IsMatch(whoisRawText, @"\bpostal code\b", RegexOptions.IgnoreCase) ||
                   Regex.IsMatch(whoisRawText, @"\bcountry\b", RegexOptions.IgnoreCase);
        }

        private static bool IsOnlySimpleAddress(string whoisRawText)
        {
            return Regex.IsMatch(whoisRawText, @"\baddress\b", RegexOptions.IgnoreCase);
        }
    }
}
