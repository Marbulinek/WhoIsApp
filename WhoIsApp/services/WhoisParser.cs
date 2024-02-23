using System.Text.RegularExpressions;
using WhoIsApp.models;

namespace WhoIsApp.services
{
    public class WhoisParser
    {
        private ModelManager _modelManager;
        public WhoisParser()
        {
            _modelManager = new ModelManager();
        }

        public WhoIsResult Parse(string whoisRawText)
        {
            WhoIsResult whoisResult = new WhoIsResult();

            // prepare model
            _modelManager.PrepareModel(whoisRawText);

            // dates
            string[] parameters_domain_name = { "domain", "domain name" };
            whoisResult.Domain.Name = _modelManager.SearchModel(parameters_domain_name);

            string[] parameters_created_date = { "creation date", "created", "registered" };
            whoisResult.Domain.CreatedDate = _modelManager.SearchModel(parameters_created_date).FormatDateTime();

            string[] parameters_update_date = { "updated date", "updated", "changed", "last update" };
            whoisResult.Domain.LastUpdateDate = _modelManager.SearchModel(parameters_update_date).FormatDateTime();

            string[] parameters_expiry_date = { "valid until", "expire", "expire date", "expiration date" };
            whoisResult.Domain.ExpireDate = _modelManager.SearchModel(parameters_expiry_date).FormatDateTime();


            // registrant
            string[] parameters_registrant_name = { "name", "registrant name" };
            whoisResult.Registrant.Name = _modelManager.SearchModel(parameters_registrant_name);

            string[] parameters_registrant_email = { "email", "mail" };
            whoisResult.Registrant.Email = _modelManager.SearchModel(parameters_registrant_email);

            string[] parameters_registrant_organization = { "organization", "org", "registrant organization" };
            whoisResult.Registrant.Organization.Name = _modelManager.SearchModel(parameters_registrant_organization);

            // address
            if ((Regex.IsMatch(whoisRawText, @"\bstreet\b", RegexOptions.IgnoreCase)) || (Regex.IsMatch(whoisRawText, @"\bcity\b", RegexOptions.IgnoreCase)) || (Regex.IsMatch(whoisRawText, @"\bpostal code\b", RegexOptions.IgnoreCase)) || (Regex.IsMatch(whoisRawText, @"\bcountry\b", RegexOptions.IgnoreCase)))
            {
                //registar organization address
                string[] parameters_registrant_address_street = { "street", "registrant street" };
                whoisResult.Registrant.Address.Street = _modelManager.SearchModel(parameters_registrant_address_street);

                string[] parameters_registrant_address_city = { "city", "registrant city" };
                whoisResult.Registrant.Address.City = _modelManager.SearchModel(parameters_registrant_address_city);

                string[] parameters_registrant_address_postal = { "postal code", "registrant postal code" };
                whoisResult.Registrant.Address.PostalCode = _modelManager.SearchModel(parameters_registrant_address_postal);

                string[] parameters_registrant_address_country_code = { "country code", "country", "registrant country" };
                whoisResult.Registrant.Address.CountryCode = _modelManager.SearchModel(parameters_registrant_address_country_code);
            }
            else if (Regex.IsMatch(whoisRawText, @"\baddress\b", RegexOptions.IgnoreCase))
            {
                string[] parameters_registrant_address = { "address", "registrant address" };
                whoisResult.Registrant.Address.Street = _modelManager.SearchModel(parameters_registrant_address);
                whoisResult.Registrant.Address.City = _modelManager.SearchModel(parameters_registrant_address);
                whoisResult.Registrant.Address.PostalCode = _modelManager.SearchModel(parameters_registrant_address);
                whoisResult.Registrant.Address.CountryCode = _modelManager.SearchModel(parameters_registrant_address);
            }

            return whoisResult;
        }
    }
}
