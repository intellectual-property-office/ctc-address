using IPO.Address.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using IPO.Address.Models;

namespace IPO.Address.BDDTests.Helpers
{
    public class MockedAddressGateway : IAddressGateway
    {
        public async Task<IEnumerable<AddressResult>> GetAddressesAsync(string countryCode, string searchTerm)
        {
            return await Task.FromResult(new List<AddressResult> { new AddressResult { Address = "TestAddress" } });
        }
    }
}
