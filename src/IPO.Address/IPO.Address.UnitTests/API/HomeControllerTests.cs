using AwesomeAssertions;
using IPO.Address.API.Controllers;
using IPO.Address.Interfaces;
using IPO.Address.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace IPO.Address.UnitTests.API
{
    [TestClass]
    public class HomeControllerTests
    {
        private readonly Mock<IAddressService> _mockAddressService;

        public HomeControllerTests()
        {
            _mockAddressService = new Mock<IAddressService>();
        }

        [TestMethod]
        public async Task GetAddressListReturnsOK()
        {
            // Arrrange
            var countryList = new List<AddressResult>() { new AddressResult { Address = "Intellectual Property Office, Unit 6, Nine Mile Point Industrial Estate, NEWPORT, NP11 7HZ" } };

            _mockAddressService.Setup(s => s.GetAddressesAsync(It.IsAny<string>(),It.IsAny<string>()))
                               .ReturnsAsync(countryList).Verifiable();

            var countriesApi = new HomeController(_mockAddressService.Object);

            // Act
            var addressList = await countriesApi.GetAddressList("Intellectual", "NP11");
            var countriesResult = (OkObjectResult)addressList.Result!;
            var results = (IEnumerable<AddressResult>)countriesResult.Value!;

            // Assert
            results.Should().ContainInOrder(countryList);
            countriesResult.StatusCode.Should().NotBeNull();
            countriesResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            _mockAddressService.Verify();
        }
    }
}
