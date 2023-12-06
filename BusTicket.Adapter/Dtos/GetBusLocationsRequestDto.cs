using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusTicket.Adapter.Dtos
{
    public class GetBusJourneysRequestDto
    {
        [JsonPropertyName("device-session")]
        public DeviceSession DeviceSession { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("data")]
        public DataModel Data { get; set; }

        public class DataModel
        {
            [JsonPropertyName("origin-id")]
            public int OriginId { get; set; }

            [JsonPropertyName("destination-id")]
            public int DestinationId { get; set; }

            [JsonPropertyName("departure-date")]
            public string DepartureDate { get; set; }
        }
    }
}
