using IPO.Address.Models.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace IPO.Address.UnitTests
{
    public static class SettingsFactory
    {
        public static Stream BuildAsJsonStream()
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Build())));
        }
        public static Settings Build()
        {
            var settings = new Settings
            {
                MelissaGateway = new MelissaGatewayOptions()
            };
            settings.MelissaGateway.MelissaConnectionString = "http://expressentry.melissadata.net/web/GlobalExpressFreeForm";
            settings.MelissaGateway.MelissaId = "1";
            settings.MelissaGateway.MelissaMaxRecords = 200;

            return settings;
        }
    }
}
