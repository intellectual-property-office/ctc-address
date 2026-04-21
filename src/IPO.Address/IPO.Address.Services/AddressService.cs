using IPO.Address.Interfaces;
using IPO.Address.Models;

namespace IPO.Address.Services
{
    public class AddressService : IAddressService
    {
        public IAddressGateway AddressGateway { get; }

        public AddressService(IAddressGateway addressGateway)
        {
            AddressGateway = addressGateway;
        }

        public async Task<IEnumerable<AddressResult>> GetAddressesAsync(string countryCode, string searchTerm)
        {
            return await AddressGateway.GetAddressesAsync(countryCode, searchTerm);
        }
    }
}