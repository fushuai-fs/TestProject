using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Position
    {
        public string DeviceID { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Mark { get; set; }
        public DateTime? date { get; set; }
    }
}
