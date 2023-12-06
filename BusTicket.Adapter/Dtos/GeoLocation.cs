using System.Text.Json.Serialization;

namespace BusTicket.Adapter.Dtos
{
    public class GeoLocation
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("zoom")]
        public int Zoom { get; set; }
    }


}
