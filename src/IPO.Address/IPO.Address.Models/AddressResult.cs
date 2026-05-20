using Swashbuckle.AspNetCore.Annotations;


namespace IPO.Address.Models

{
    [SwaggerSchema(Description = "The response body for the Get Addresses endpoint")]
    public class AddressResult
    {
        [SwaggerSchema(Title = "Comma separated representation of the full address")]
        public string? Address { get; set; }

        [SwaggerSchema(Title = "First line of address")]
        public string? Address1 { get; set; }

        [SwaggerSchema(Title = "Second line of address")]
        public string? Address2 { get; set; }

        [SwaggerSchema(Title = "Third line of address")]
        public string? Address3 { get; set; }

        [SwaggerSchema(Title = "Fourth line of address")]
        public string? Address4 { get; set; }

        [SwaggerSchema(Title = "Fifth line of address")]
        public string? Address5 { get; set; }

        [SwaggerSchema(Title = "Sixth line of address")]
        public string? Address6 { get; set; }

        [SwaggerSchema(Title = "Seventh line of address")]
        public string? Address7 { get; set; }

        [SwaggerSchema(Title = "Eighth line of address")]
        public string? Address8 { get; set; }

        [SwaggerSchema(Title = "ISO 3166 country code")]
        public string? ISO3166_2 { get; set; }

        [SwaggerSchema(Title = "Super administrative area. Usually country")]
        public string? SuperAdministrativeArea { get; set; }

        [SwaggerSchema(Title = "Administrative area")]
        public string? AdministrativeArea { get; set; }

        [SwaggerSchema(Title = "Sub administrative area")]
        public string? SubAdministrativeArea { get; set; }

        [SwaggerSchema(Title = "Locality")]
        public string? Locality { get; set; }

        [SwaggerSchema(Title = "Dependent locality")]
        public string? DependentLocality { get; set; }

        [SwaggerSchema(Title = "Postal code")]
        public string? PostalCode { get; set; }

        [SwaggerSchema(Title = "Organisation name")]
        public string? Organization { get; set; }

        [SwaggerSchema(Title = "Unique Melissa Address Key")]
        public string? MAK { get; set; }


    }
}
