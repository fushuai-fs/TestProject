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
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }
        public string Mark { get; set; }
        private DateTime? _date;
        public DateTime? date { set { _date = value; } get { return _date; } }

        /// <summary>
        /// 高德经度
        /// </summary>
        public string GaodeLongitude { get; set; }
        /// <summary>
        /// 高德纬度
        /// </summary>
        public string GaodeLatitude { get; set; }
    }
}
