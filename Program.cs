
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<tests> list = new List<tests>();
            tests test = new tests("jack1", 1100); test.AddTime = DateTime.Now; list.Add(test);
            test = new tests("jack2", 2000); test.AddTime = DateTime.Now; list.Add(test);
            test = new tests("jack3", 2000); test.AddTime = DateTime.Now; list.Add(test);
            //  MongoDBTest.MongoDBHelper.InserOne("hotel", "bar", test);
            DataAccessor.MongoHelper.InsertMany("hotel", "bar", list);
            // FilterDefinition<tests> filter = Builders<tests>.Filter.Eq("age", 1000);
            // UpdateDefinition<tests> update = Builders<tests>.Update.Set("name", "testupdate1").CurrentDate("ModifyTime");
            // MongoDBHelper.UpdateMany("hotel", "bar", filter, update);
            //  MongoDBHelper.Insert1();

            Console.ReadKey();
        }
    }


    public class tests
    {
        public MongoDB.Bson.ObjectId id { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime ModifyTime { get; set; }

        public tests(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public string name { get; set; }
        public int age { get; set; }
        public bool sex { get; set; }
    }
}
