using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.Business.Dtos
{
    public class BusJourneysGetInput
    {
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public DateTime Date { get; set; }
    }
}
