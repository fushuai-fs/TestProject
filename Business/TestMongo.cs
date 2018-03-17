using DataAccessor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TestMongo
    {
        public static void log()
        {
            string sql = string.Format("SELECT * FROM dbo.HotelBeds_Interface_log WITH(NOLOCK) WHERE AddTime >'{0}'", "2018-01-01");
            DataTable dt = SQLHelper.ExecuteDataset(SQLHelper.ConnectionString, CommandType.Text, sql, null).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string Type = row["Type"].ToString();
                    int OrderID = Convert.ToInt32(row["OrderID"]);
                    dynamic Request;
                    if (Type == "GetHotelBedsOrders")
                    {
                        Request = row["Request"].ToString();
                    }
                    else
                    {
                        Request = JsonConvert.DeserializeObject<dynamic>(row["Request"].ToString());
                    }
                  //  dynamic Response = JsonConvert.DeserializeObject<dynamic>(row["Response"].ToString());
                    dynamic Response = row["Response"].ToString();
                    DateTime AddTime = Convert.ToDateTime(row["AddTime"]);

                    Entity.Position position = new Entity.Position ();
                    position.DeviceID = "";position.Latitude = "";position.Longitude = "";position.Mark = "";
                    position.date = DateTime.Now;
                    
                    object obj = new { Type = Type, OrderID = OrderID, Request = Request, Response = Response, AddTime = AddTime, position= position };

                    DataAccessor.MongoDBHelper.InserOne("hotel", Type, obj);

                }
            }
        }
    }
}
