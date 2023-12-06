using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusTicket.Adapter.Dtos
{
    public class GetSessionRequestDto
    {
        [JsonPropertyName("type")]
        public long Type { get; set; } = 1;

        [JsonPropertyName("connection")]
        public ConnectionModel Connection { get; set; }

        [JsonPropertyName("browser")]
        public BrowserModel Browser { get; set; }

        public partial class BrowserModel
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("version")]
            public string Version { get; set; }
        }

        public partial class ConnectionModel
        {
            [JsonPropertyName("ip-address")]
            public string IpAddress { get; set; }

            [JsonPropertyName("port")]
            public string Port { get; set; }
        }
    }
 
    public class GetBusLocationsRequestDto
    {
        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("device-session")]
        public DeviceSession DeviceSession { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }
    }

}
