namespace WhoIsApp.Test
{
    [TestClass]
    public class WhoIsTest
    {
        private WhoisManager _whoisManager;


        [TestInitialize]
        public void Initialize()
        {
            _whoisManager = new WhoisManager();
        }


        [TestMethod]
        public void Property_Mapping_IT_Test()
        {
            var result = _whoisManager.RetrieveData("thelocal.it");
            Assert.IsNotNull(result.Payload);
            Assert.AreEqual("thelocal.it", result.Payload.Domain.Name);
            Assert.AreEqual("2006-11-13 00:00:00", result.Payload.Domain.CreatedDate);
            Assert.AreEqual("2023-11-29 00:51:11", result.Payload.Domain.LastUpdateDate);
            Assert.AreEqual("2024-11-13", result.Payload.Domain.ExpireDate);

            Assert.AreEqual("The Local Europe AB", result.Payload.Registrant.Organization.Name);

            Assert.AreEqual("iOffice, Vasagatan 10", result.Payload.Registrant.Address.Street);
            Assert.AreEqual("Stockholm", result.Payload.Registrant.Address.City);
            Assert.AreEqual("11120", result.Payload.Registrant.Address.PostalCode);
            Assert.AreEqual("SE", result.Payload.Registrant.Address.CountryCode);
        }


        [TestMethod]
        public void Property_Mapping_CZ_Test()
        {
            var result = _whoisManager.RetrieveData("sport.cz");
            Assert.IsNotNull(result);
            Assert.AreEqual("sport.cz", result.Payload.Domain.Name);
            Assert.AreEqual("1997-07-12 02:00:00", result.Payload.Domain.CreatedDate);
            Assert.AreEqual("2022-09-05 14:20:46", result.Payload.Domain.LastUpdateDate);
            Assert.AreEqual("2024-10-24", result.Payload.Domain.ExpireDate);

            Assert.AreEqual("Pavel Zima", result.Payload.Registrant.Name);
            Assert.AreEqual("Seznam.cz, a.s.", result.Payload.Registrant.Organization.Name);

            Assert.AreEqual("Radlická 3294/10", result.Payload.Registrant.Address.Street);
            Assert.AreEqual("Praha 5", result.Payload.Registrant.Address.City);
            Assert.AreEqual("150 00", result.Payload.Registrant.Address.PostalCode);
            Assert.AreEqual("CZ", result.Payload.Registrant.Address.CountryCode);
        }


        [TestMethod]
        public void Property_Mapping_SK_Test()
        {
            var result = _whoisManager.RetrieveData("lukasoft.sk");
            Assert.IsNotNull(result);
            Assert.AreEqual("lukasoft.sk", result.Payload.Domain.Name);
            Assert.AreEqual("2022-01-20", result.Payload.Domain.CreatedDate);
            Assert.AreEqual("2024-02-12", result.Payload.Domain.LastUpdateDate);
            Assert.AreEqual("2025-01-20", result.Payload.Domain.ExpireDate);

            Assert.AreEqual("Ing Lukas Caniga", result.Payload.Registrant.Name);
            Assert.AreEqual("LukaSoft s. r. o.", result.Payload.Registrant.Organization.Name);

            Assert.AreEqual("Rosinska 8541/30A", result.Payload.Registrant.Address.Street);
            Assert.AreEqual("Zilina", result.Payload.Registrant.Address.City);
            Assert.AreEqual("01008", result.Payload.Registrant.Address.PostalCode);
            Assert.AreEqual("SK", result.Payload.Registrant.Address.CountryCode);
        }
    }
}