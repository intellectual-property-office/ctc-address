using IPO.Address.Models;

namespace IPO.Address.Interfaces
{
    public interface IAddressGateway
    {
        Task<IEnumerable<AddressResult>> GetAddressesAsync(string countryCode, string searchTerm);
    }
}
