using IPO.Address.Models;

namespace IPO.Address.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressResult>> GetAddressesAsync(string countryCode, string searchTerm);
    }
}
