using System.Text.Json.Serialization;

namespace BusTicket.Adapter.Dtos
{
    public class DeviceSession
    {
        [JsonPropertyName("session-id")]
        public string SessionId { get; set; }

        [JsonPropertyName("device-id")]
        public string DeviceId { get; set; }
    }
}
