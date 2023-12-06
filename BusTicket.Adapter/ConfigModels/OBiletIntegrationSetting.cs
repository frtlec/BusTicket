using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.Adapter.ConfigModels
{
    public class OBiletIntegrationSetting
    {
        public string BaseUrl { get; set; }
        public string GetSessionUrl { get; set; }
        public string BusLocationsUrl { get; set; }
        public string BusJourneysUrl { get; set; }
    }
}
