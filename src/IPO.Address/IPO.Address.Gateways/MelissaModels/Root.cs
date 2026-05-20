namespace IPO.Address.Gateways.MelissaModels
{
    public class Root
    {
        public string? Version { get; set; }
        public string? ResultCode { get; set; }
        public string? ErrorString { get; set; }
        public List<Result>? Results { get; set; }
    }
}
