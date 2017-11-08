using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public static class MongodbTest
    {
        public static void test()
        {
            List<testModel> list = new List<testModel>();
            testModel test = new testModel("jack1", 1100); test.AddTime = DateTime.Now; list.Add(test);
            test = new testModel("jack2", 2000); test.AddTime = DateTime.Now; list.Add(test);
            test = new testModel("jack3", 2000); test.AddTime = DateTime.Now; list.Add(test);
            //  MongoDBTest.MongoDBHelper.InserOne("hotel", "bar", test);
            DataAccessor.MongoDBHelper.InsertMany("hotel", "bar", list);
            // FilterDefinition<testModel> filter = Builders<testModel>.Filter.Eq("age", 1000);
            // UpdateDefinition<testModel> update = Builders<testModel>.Update.Set("name", "testupdate1").CurrentDate("ModifyTime");
            // MongoDBHelper.UpdateMany("hotel", "bar", filter, update);
            //  MongoDBHelper.Insert1();
        }
    }

      public class testModel
    {
        public MongoDB.Bson.ObjectId id { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime ModifyTime { get; set; }

        public testModel(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public string name { get; set; }
        public int age { get; set; }
        public bool sex { get; set; }
    }
}
