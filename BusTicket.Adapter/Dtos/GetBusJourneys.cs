using System.Text.Json.Serialization;

namespace BusTicket.Adapter.Dtos
{
    public class GetBusJourneys
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("journey")]
        public Journey Journey { get; set; }

        [JsonPropertyName("origin-location")]
        public string OriginLocation { get; set; }
        [JsonPropertyName("origin-location-id")]
        public int OriginLocationId { get; set; }


        [JsonPropertyName("destination-location")]
        public string DestinationLocation { get; set; }
        [JsonPropertyName("destination-location-id")]
        public int DestinationLocationId { get; set; }

        [JsonPropertyName("is-active")]
        public bool IsActive { get; set; }

    }

    public class Journey
    {
        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("destination")]
        public string Destination { get; set; }

        [JsonPropertyName("departure")]
        public DateTime Departure { get; set; }

        [JsonPropertyName("arrival")]
        public DateTime Arrival { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("original-price")]
        public decimal OriginalPrice { get; set; }
    }
}
