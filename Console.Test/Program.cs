using Common;
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
            // MongodbTest.test();

            List<testModel> list = new List<testModel>();
            testModel m = new testModel("jack", 18);
            list.Add(m);
            list.Add(m);
            m = new testModel("jack", 20);
            list.Add(m);
            m = new testModel("jack", 21);
            list.Add(m);

            Console.WriteLine("list.count=" + list.Count);

            list = list.Distinct(new DistinctComparer<testModel>("age")).ToList(); 
            Console.WriteLine("list.count=" + list.Count);

            //list = list.Distinct(new DistinctComparer<testModel>("name")).ToList();
            //Console.WriteLine("list.count=" + list.Count);

            List<testModel> list2 = new List<testModel>();
            m = new testModel("jack", 18);
            list2.Add(m);
            m = new testModel("jack11", 20);
            list2.Add(m);
            EnumerableCompare<testModel> comparer = new EnumerableCompare<testModel>((x, y) => x.name == y.name);
            list = list2.Except(list,comparer).ToList(); 
            Console.WriteLine("list.count=" + list.Count);
            Console.WriteLine("list2.count=" + list2.Count);

            Console.ReadKey();
        }
    }

}
