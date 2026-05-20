using IPO.Address.Interfaces;
using IPO.Address.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace IPO.Address.API.HealthChecks
{
    public class AddressServiceHealthCheck : IHealthCheck
    {
        private readonly IAddressService _addressService;
        private readonly IConfiguration _configuration;

        public AddressServiceHealthCheck(IAddressService addressService, IConfiguration configuration)
        {
            _addressService = addressService;
            _configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // App config settings injected by Terraform
            var countryCode = _configuration.GetSection("HealthReady")["CountryCode"]!.ToString();
            var searchTerm = _configuration.GetSection("HealthReady")["SearchTerm"]!.ToString();

            var result = await _addressService.GetAddressesAsync(countryCode, searchTerm);

            var isHealthy = result != null &&
                result is IEnumerable<AddressResult> &&
                result.Any();

            return isHealthy
                ? HealthCheckResult.Healthy("Address provider is available")
                : HealthCheckResult.Unhealthy("Address provider is not available");
        }
    }
}