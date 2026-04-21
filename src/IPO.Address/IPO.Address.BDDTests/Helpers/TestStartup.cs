using IPO.Address.API;
using IPO.Address.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IPO.Address.BDDTests.Helpers
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        { }

        public static TestServer GetTestServer()
        {
            var hostBuilder = new HostBuilder()
               .ConfigureWebHost(webHost =>
               {
                   webHost
                      .UseTestServer()
                      .UseStartup<TestStartup>();
               });
            var host = hostBuilder.Start();
            return host.GetTestServer();
        }

        protected override void AddManagementServices(IServiceCollection services)
        {
            services.AddScoped<IAddressGateway, MockedAddressGateway>();
        }
    }
}
