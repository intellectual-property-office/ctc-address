using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IPO.Address.API.HealthChecks;
using IPO.Address.Interfaces;
using IPO.Address.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IPO.Address.UnitTests.API
{
    [TestClass]
    public class AddressServiceHealthCheckTests
    {
        private Mock<IAddressService>? _mockAddressService;
        private IConfiguration? _configuration;
        private AddressServiceHealthCheck? _uut;
        private List<AddressResult>? _addressList;

        private readonly string _countryCode;
        private readonly string _searchTerm;

        public AddressServiceHealthCheckTests()
        {
            _countryCode = "GB";
            _searchTerm = "NP108QQ";
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _mockAddressService = new Mock<IAddressService>();

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(
                    new Dictionary<string, string?>
                    {
                        { "HealthReady:CountryCode", _countryCode },
                        { "HealthReady:SearchTerm", _searchTerm }
                    }
                 )
                .Build();

            _uut = new AddressServiceHealthCheck(_mockAddressService.Object, _configuration);

            _addressList = new List<AddressResult>();
        }

        [TestMethod]
        public async Task ReadyReturnsAvailableIfAddresessFound()
        {
            // Arrange

            _addressList!.Add(new AddressResult
            {
                Address = "The Intellectual Property Office, Concept House, Cardiff Road, NEWPORT, NP10 8QQ",
                Address1 = "Concept House",
                Address2 = "Cardiff Road",
                Address3 = "Newport",
                Address4 = "NP10 8QQ",
                Address5 = "",
                Address6 = "",
                Address7 = "",
                Address8 = "",
                ISO3166_2 = "GB",
                SuperAdministrativeArea = "Wales",
                AdministrativeArea = "Newport",
                SubAdministrativeArea = "",
                Locality = "Newport",
                DependentLocality = "",
                PostalCode = "NP10 8QQ",
                Organization = "The Intellectual Property Office",
                MAK = "8247267536"
            });

            _mockAddressService!.Setup(e => e.GetAddressesAsync(_countryCode, _searchTerm)).ReturnsAsync(_addressList).Verifiable();

            // Act

            var actual = await _uut!.CheckHealthAsync(null!, CancellationToken.None);

            // Assert

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(HealthCheckResult));
            Assert.AreEqual("Address provider is available", actual.Description);
            Assert.AreEqual(HealthStatus.Healthy, actual.Status);
            Assert.IsNull(actual.Exception);
            _mockAddressService.Verify();
        }

        [TestMethod]
        public async Task ReadyReturnsUnavailableIfAddresessNotFound()
        {
            // Arrange

            _mockAddressService!.Setup(e => e.GetAddressesAsync(_countryCode, _searchTerm)).ReturnsAsync(_addressList!).Verifiable();

            // Act

            var actual = await _uut!.CheckHealthAsync(null!, CancellationToken.None);

            // Assert

            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(HealthCheckResult));
            Assert.AreEqual("Address provider is not available", actual.Description);
            Assert.AreEqual(HealthStatus.Unhealthy, actual.Status);
            Assert.IsNull(actual.Exception);
            _mockAddressService.Verify();
        }
    }
}