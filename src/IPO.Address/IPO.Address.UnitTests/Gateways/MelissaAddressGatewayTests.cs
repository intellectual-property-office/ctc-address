using AwesomeAssertions;
using IPO.Address.Gateways;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IPO.Address.UnitTests.Gateways
{
    [TestClass]
    public class MelissaAddressGatewayTests
    {
        private readonly Mock<GetMelissaQueryString> _mockGetMelissaQueryString;

        public MelissaAddressGatewayTests()
        {
            this._mockGetMelissaQueryString = new Mock<GetMelissaQueryString>();
        }

        [TestMethod]
        public async Task GetAddressesAsyncReturnsOk()
        {
            // Arrange
            var  queryString = "?id=&country=aa&ff=a&nativecharset=true&maxrecords=200&format=json";
            var outputMak = "5080503164";

            _mockGetMelissaQueryString.Setup(o => o(It.IsAny<string>(), It.IsAny<string>()))
                                      .Returns(queryString).Verifiable();

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("{\"Version\":\"4.6.8.1173\",\"ResultCode\":\"XS01\",\"ErrorString\":\"\",\"Results\":[{\"Address\":{\"Address\":\"IntellectualPropertyOffice,Unit6,NineMilePointIndustrialEstate,NEWPORT,NP117HZ\",\"Address1\":\"IntellectualPropertyOffice\",\"Address2\":\"Unit6\",\"Address3\":\"NineMilePointIndustrialEstate\",\"Address4\":\"NEWPORT\",\"Address5\":\"NP117HZ\",\"Address6\":\"\",\"Address7\":\"\",\"Address8\":\"\",\"Address9\":\"\",\"Address10\":\"\",\"Address11\":\"\",\"Address12\":\"\",\"DeliveryAddress\":\"IntellectualPropertyOffice,Unit6,NineMilePointIndustrialEstate\",\"DeliveryAddress1\":\"IntellectualPropertyOffice\",\"DeliveryAddress2\":\"Unit6\",\"DeliveryAddress3\":\"NineMilePointIndustrialEstate\",\"DeliveryAddress4\":\"\",\"DeliveryAddress5\":\"\",\"DeliveryAddress6\":\"\",\"DeliveryAddress7\":\"\",\"DeliveryAddress8\":\"\",\"DeliveryAddress9\":\"\",\"DeliveryAddress10\":\"\",\"DeliveryAddress11\":\"\",\"DeliveryAddress12\":\"\",\"CountryName\":\"UnitedKingdom\",\"ISO3166_2\":\"GB\",\"ISO3166_3\":\"GBR\",\"ISO3166_N\":\"826\",\"SuperAdministrativeArea\":\"Wales\",\"AdministrativeArea\":\"Monmouthshire\",\"SubAdministrativeArea\":\"\",\"Locality\":\"Newport\",\"CityAccepted\":\"\",\"CityNotAccepted\":\"\",\"DependentLocality\":\"Ynysddu\",\"DoubleDependentLocality\":\"Cwmfelinfach\",\"Thoroughfare\":\"NineMilePointIndustrialEstate\",\"DependentThoroughfare\":\"\",\"Building\":\"\",\"Premise\":\"\",\"SubBuilding\":\"Unit6\",\"PostalCode\":\"NP117HZ\",\"PostalCodePrimary\":\"NP117HZ\",\"PostalCodeSecondary\":\"\",\"Organization\":\"IntellectualPropertyOffice\",\"PostBox\":\"\",\"Unmatched\":\"\",\"GeneralDelivery\":\"\",\"DeliveryInstallation\":\"\",\"Route\":\"\",\"AdditionalContent\":\"\",\"CountrySubdivisionCode\":\"GB-CAY\",\"MAK\":\"5080503164\",\"BaseMAK\":\"\",\"DistanceFromPoint\":0}}]}"),
               })
               .Verifiable();

            var client = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://expressentry.melissadata.net/web/GlobalExpressFreeForm")
            };

            var melissaAddressGateway = new MelissaAddressGateway(client, this._mockGetMelissaQueryString.Object);

            // Act
            var result = await melissaAddressGateway.GetAddressesAsync("Intellectual", "NP11");

            // Assert
            result.Should().NotBeNull();
            result.FirstOrDefault()!.MAK.Should().Be(outputMak);
        }

    }
}
