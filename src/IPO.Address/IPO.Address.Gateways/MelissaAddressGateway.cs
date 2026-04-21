using IPO.Address.Gateways.MelissaModels;
using IPO.Address.Interfaces;
using System.Text.Json;

namespace IPO.Address.Gateways
{
    public class MelissaAddressGateway : IAddressGateway
    {
        public HttpClient Client { get; }
        public GetMelissaQueryString GetMelissaQueryString { get; }

        public MelissaAddressGateway(HttpClient httpClient, GetMelissaQueryString getMelissaQueryString)
        {
            Client = httpClient;
            GetMelissaQueryString = getMelissaQueryString;
        }

        public async Task<IEnumerable<Models.AddressResult>> GetAddressesAsync(string countryCode, string searchTerm)
        {
            var queryString = GetMelissaQueryString(countryCode, searchTerm);
            var response = await Client.GetAsync(queryString);
            var rawcontent = await response.Content.ReadAsStringAsync();
            var melissaResult = JsonSerializer.Deserialize<Root>(rawcontent);
			var result = melissaResult?.Results == null
				? new List<Models.AddressResult>()
				: melissaResult.Results
					.Where(x => x.Address != null)
					.Select(x => x.Address!.ToModel())
					.ToList();


			return result;
        }
    }

    public delegate string GetMelissaQueryString(string countryCode, string searchTerm);
}