using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.Business.Dtos
{
    public class GetJourneysResponse
    {
        public string OriginLocation { get; set; }
        public string DestinationLocation { get; set; }
        public DateTime JourneysDate { get; set; }
        public List<Item> Items { get; set; } = new();
        public class Item
        {
            public TimeSpan Arrival { get; set; }
            public TimeSpan Departure { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
            public decimal Price { get; set; }
            public string Currency { get; set; }
        }
    }
}
