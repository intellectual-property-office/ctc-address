using IPO.Address.Interfaces;
using IPO.Address.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IPO.Address.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        public IAddressService AddressService { get; }

        public HomeController(IAddressService addressService)
        {
            AddressService = addressService;
        }

        [SwaggerOperation(Summary = "Postal address search funtion." ,
                          Description = "**Notes:** \n\n A search function that returns matching addresses based on the search terms. \n\nAn ISO country code and postcode provide the most accurate results, however a country name and any part of an address can be used as search terms.")]
        [Produces("application/json")]
        [HttpGet]
        [Route("{countryCode}/addresses/{search}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AddressResult>>> GetAddressList(string countryCode, string search)
        {
            var result = await AddressService.GetAddressesAsync(countryCode, search);
            return Ok(result);
        }
    }
}
