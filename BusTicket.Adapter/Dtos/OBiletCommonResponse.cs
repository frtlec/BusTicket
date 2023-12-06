﻿using System.Text.Json.Serialization;

namespace BusTicket.Adapter.Dtos
{
    public class OBiletCommonResponseDto<T>
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("data")]
        public virtual T Data { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("user-message")]
        public string UserMessage { get; set; }

        [JsonPropertyName("api-request-id")]
        public string ApiRequestId { get; set; }

        [JsonPropertyName("controller")]
        public string Controller { get; set; }

        [JsonPropertyName("client-request-id")]
        public string ClientRequestId { get; set; }

        [JsonPropertyName("web-correlation-id")]
        public string WebCorrelationId { get; set; }

        [JsonPropertyName("correlation-id")]
        public string CorrelationId { get; set; }

        [JsonPropertyName("parameters")]
        public object Parameters { get; set; }
    }


}
