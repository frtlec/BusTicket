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
        public string GetSettionUrl
        {
            get {  return this.GetSettionUrl; }
            set { this.GetSettionUrl = value; }
        }
        public string BusLocationsUrl
        {
            get { return  this.BusLocationsUrl; }
            set { this.BusLocationsUrl = value; }
        }
    }
}
