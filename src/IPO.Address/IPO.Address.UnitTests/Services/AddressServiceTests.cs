using AwesomeAssertions;
using IPO.Address.Interfaces;
using IPO.Address.Models;
using IPO.Address.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPO.Address.UnitTests.Services
{
    [TestClass]
    public class AddressServiceTests
    {
        private readonly Mock<IAddressGateway> _mockAddressGateway;
        private readonly AddressService _addressService;

        public AddressServiceTests()
        {
            this._mockAddressGateway = new Mock<IAddressGateway>();

            _addressService = new AddressService(this._mockAddressGateway.Object);
        }

        [TestMethod]
        public async Task GetAddressesAsyncReturnsOK()
        {
            // Arrange
            var aadressList = new List<AddressResult>() { new AddressResult { Address = "Intellectual Property Office, Unit 6, Nine Mile Point Industrial Estate, NEWPORT, NP11 7HZ" } };

            this._mockAddressGateway.Setup(o => o.GetAddressesAsync(It.IsAny<string>(), It.IsAny<string>()))
                                    .ReturnsAsync(aadressList);

            // Act
            var results = await _addressService.GetAddressesAsync("Intellectual", "NP11");

            // Assert
            results.Should().ContainInOrder(aadressList);
        }

    }
}
