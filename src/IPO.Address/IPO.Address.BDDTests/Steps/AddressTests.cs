using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using AwesomeAssertions;
using System.Net;
using IPO.Address.BDDTests.Helpers;
using System.Net.Http.Json;
using IPO.Address.Models;
using System.Collections.Generic;
using System.Linq;
using Reqnroll;

namespace IPO.Address.BDDTests
{
    [Binding]
    public class AddressTests
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public AddressTests(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _server = TestStartup.GetTestServer();
            _client = _server.CreateClient();
        }

        [Given(@"there is a Country Code ""([^""]*)"" and search ""([^""]*)""")]
        public void GivenThereIsACountryCodeAndSearch(string countryCode, string search)
        {
            _scenarioContext.Add("countryCode", countryCode);
            _scenarioContext.Add("search", search);
        }

        [When(@"api Addresses search requested")]
        public async Task WhenApiURLCountryCodeAddressesSearchRequested()
        {
            var countrycode = _scenarioContext["countryCode"].ToString();
            var search = _scenarioContext["search"].ToString();

            var url = $"/{countrycode}/Addresses/{search}";

            var postResponse = await _client.GetAsync(url);

            _scenarioContext.Add("ResponseStatusCode", postResponse.StatusCode);
            _scenarioContext.Add("ResponseContent", postResponse.Content);
        }

        [Then(@"The List of Mock Addresses is Returned")]
        public async Task ThenTheListOfAddressesIsReturned()
        {
            _scenarioContext["ResponseStatusCode"].Should().Be(HttpStatusCode.OK);
            var expectedpaymentAddress = "TestAddress";

            var content = _scenarioContext.Get<HttpContent>("ResponseContent");
            var result = await content.ReadFromJsonAsync<List<AddressResult>>();
            result!.Any(x => x.Address == expectedpaymentAddress).Should().BeTrue();
        }
    }
}
