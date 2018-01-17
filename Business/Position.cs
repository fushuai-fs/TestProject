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
           string data = Newtonsoft.Json.JsonConvert.SerializeObject(position);
            string path = @"E:\log\"+DateTime.Today.ToString("yyyyMMdd")+@"\test.txt";
            if (!Directory.Exists(Path.GetDirectoryName(path))) { Directory.CreateDirectory(Path.GetDirectoryName(path)); }
            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            { using (StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.UTF8)) { sw.WriteLine(data); } }

            return DataAccessor.MongoDBHelper.InserOne("hotel","Position", position);
        }
    }
}
