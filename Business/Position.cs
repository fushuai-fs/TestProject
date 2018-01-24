using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Position
    {
        public static bool Save(Entity.Position position)
        {
            string Uri = string.Format("http://restapi.amap.com/v3/assistant/coordinate/convert?key=8cc2b2b6f706487c45016713e3667ef5&locations={0},{1}&coordsys=gps", position.Longitude, position.Latitude);
           
            try
            {
                byte[] arrs = Common.httphelper.httpGet(Uri);
                string resultInfo = System.Text.Encoding.UTF8.GetString(arrs);
                /*{
        "status": "1",
        "info": "ok",
        "infocode": "10000",
        "locations": "116.38480577257,39.963396538629"
    }*/

                dynamic dyn = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(resultInfo);
                if (dyn.status == 1) { string[] loca = Convert.ToString(dyn.locations).Split(','); position.GaodeLongitude = loca[0]; position.GaodeLatitude = loca[1]; }
                else { position.GaodeLongitude = ""; position.GaodeLatitude = ""; }
            }
            catch (Exception e) { }

            string data = Newtonsoft.Json.JsonConvert.SerializeObject(position);
            string path = @"E:\log\" + DateTime.Today.ToString("yyyyMMdd") + @"\test.txt";
            if (!Directory.Exists(Path.GetDirectoryName(path))) { Directory.CreateDirectory(Path.GetDirectoryName(path)); }
            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            { using (StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.UTF8)) { sw.WriteLine(data); } }

            return DataAccessor.MongoDBHelper.InserOne("hotel", "Position", position);
        }
    }
}
