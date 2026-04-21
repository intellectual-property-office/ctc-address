namespace IPO.Address.Gateways.MelissaModels
{
    public class AddressResult
    {
        public string? Address { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string? Address4 { get; set; }
        public string? Address5 { get; set; }
        public string? Address6 { get; set; }
        public string? Address7 { get; set; }
        public string? Address8 { get; set; }
        public string? Address9 { get; set; }
        public string? Address10 { get; set; }
        public string? Address11 { get; set; }
        public string? Address12 { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? DeliveryAddress1 { get; set; }
        public string? DeliveryAddress2 { get; set; }
        public string? DeliveryAddress3 { get; set; }
        public string? DeliveryAddress4 { get; set; }
        public string? DeliveryAddress5 { get; set; }
        public string? DeliveryAddress6 { get; set; }
        public string? DeliveryAddress7 { get; set; }
        public string? DeliveryAddress8 { get; set; }
        public string? DeliveryAddress9 { get; set; }
        public string? DeliveryAddress10 { get; set; }
        public string? DeliveryAddress11 { get; set; }
        public string? DeliveryAddress12 { get; set; }
        public string? CountryName { get; set; }
        public string? ISO3166_2 { get; set; }
        public string? ISO3166_3 { get; set; }
        public string? ISO3166_N { get; set; }
        public string? SuperAdministrativeArea { get; set; }
        public string? AdministrativeArea { get; set; }
        public string? SubAdministrativeArea { get; set; }
        public string? Locality { get; set; }
        public string? CityAccepted { get; set; }
        public string? CityNotAccepted { get; set; }
        public string? DependentLocality { get; set; }
        public string? DoubleDependentLocality { get; set; }
        public string? Thoroughfare { get; set; }
        public string? DependentThoroughfare { get; set; }
        public string? Building { get; set; }
        public string? Premise { get; set; }
        public string? SubBuilding { get; set; }
        public string? PostalCode { get; set; }
        public string? PostalCodePrimary { get; set; }
        public string? PostalCodeSecondary { get; set; }
        public string? Organization { get; set; }
        public string? PostBox { get; set; }
        public string? Unmatched { get; set; }
        public string? GeneralDelivery { get; set; }
        public string? DeliveryInstallation { get; set; }
        public string? Route { get; set; }
        public string? AdditionalContent { get; set; }
        public string? CountrySubdivisionCode { get; set; }
        public string? MAK { get; set; }
        public string? BaseMAK { get; set; }
        public double? DistanceFromPoint { get; set; }

        public IPO.Address.Models.AddressResult ToModel()
        {
            return new IPO.Address.Models.AddressResult
            {
                Address = this.Address,
                Address1 = this.Address1,
                Address2 = this.Address2,
                Address3 = this.Address3,
                Address4 = this.Address4,
                Address5 = this.Address5,
                Address6 = this.Address6,
                Address7 = this.Address7,
                Address8 = this.Address8,
                MAK = this.MAK,
                AdministrativeArea = this.AdministrativeArea,
                DependentLocality = this.DependentLocality,
                ISO3166_2 = this.ISO3166_2,
                Locality = this.Locality,
                Organization = this.Organization,
                PostalCode = this.PostalCode,
                SubAdministrativeArea = this.SubAdministrativeArea,
                SuperAdministrativeArea = this.SuperAdministrativeArea
            };
        }
    }


}
