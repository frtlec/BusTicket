using System.Text.Json.Serialization;
using static BusTicket.Adapter.Dtos.GetBusLocationsResponseDto;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusTicket.Adapter.Dtos
{
    public partial class GetSessionResponseDto
    {
        [JsonPropertyName("session-id")]
        public string SessionId { get; set; }

        [JsonPropertyName("device-id")]
        public string DeviceId { get; set; }

        [JsonPropertyName("affiliate")]
        public object Affiliate { get; set; }

        [JsonPropertyName("device-type")]
        public long DeviceType { get; set; }

        [JsonPropertyName("device")]
        public object Device { get; set; }

        [JsonPropertyName("ip-country")]
        public string IpCountry { get; set; }
    }


}
