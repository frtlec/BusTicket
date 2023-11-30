using System.Text.Json.Serialization;

namespace BusTicket.Adapter.Dtos
{
    public partial class GetSessionResponseDto
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public DataModel Data { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("user-message")]
        public string UserMessage { get; set; }

        [JsonPropertyName("api-request-id")]
        public object ApiRequestId { get; set; }

        [JsonPropertyName("controller")]
        public object Controller { get; set; }

        [JsonPropertyName("client-request-id")]
        public object ClientRequestId { get; set; }

        [JsonPropertyName("web-correlation-id")]
        public object WebCorrelationId { get; set; }

        [JsonPropertyName("correlation-id")]
        public Guid CorrelationId { get; set; }

        [JsonPropertyName("parameters")]
        public object Parameters { get; set; }
        public partial class DataModel
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

   
}
