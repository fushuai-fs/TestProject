using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // MongodbTest.test();

            #region 比较
            //List<testModel> list = new List<testModel>();
            //testModel m = new testModel("jack", 18);
            //list.Add(m);
            //list.Add(m);
            //m = new testModel("jack", 20);
            //list.Add(m);
            //m = new testModel("jack", 21);
            //list.Add(m);

            //Console.WriteLine("list.count=" + list.Count);

            //list = list.Distinct(new DistinctComparer<testModel>("age")).ToList();
            //Console.WriteLine("list.count=" + list.Count);

            ////list = list.Distinct(new DistinctComparer<testModel>("name")).ToList();
            ////Console.WriteLine("list.count=" + list.Count);

            //List<testModel> list2 = new List<testModel>();
            //m = new testModel("jack", 18);
            //list2.Add(m);
            //m = new testModel("jack11", 20);
            //list2.Add(m);
            //EnumerableCompare<testModel> comparer = new EnumerableCompare<testModel>((x, y) => x.name == y.name);
            //list = list2.Except(list, comparer).ToList();
            //Console.WriteLine("list.count=" + list.Count);
            //Console.WriteLine("list2.count=" + list2.Count); 
            #endregion

            #region 排序  结果： list.orderby() 排序效率远高于 list.sort() 
            // Sort<T>方法的各个重载版本，最终调用的都是Array.Sort<T>(T[] array, int index, int length, IComparer<T> comparer)方法。 
            // 使用Comparison委托的时候，会执行IComparer<T> comparer = new Array.FunctorComparer<T>(comparison), 最终转换为IComparer，因此在性能上会打点折扣。
            // list.orderby() 不是在原基础上排序
            List<testModel> list = new List<testModel>();
            List<testModel> list1 = new List<testModel>();
            for (int i = 0; i < 1000000; i++)
            {
                Random R = new Random();
                int s = R.Next(0, 10000000); 
                testModel tm = new testModel("n" + s, Convert.ToInt64(s)); tm.Id = i; list.Add(tm); list1.Add(tm);
                Thread.Sleep(50);

            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            SortComparer<testModel> sc = new SortComparer<testModel>(typeof(testModel), "age", ReverserInfo.Direction.ASC);
            list.Sort(sc);
            stopWatch.Stop();
            string timeStr = stopWatch.Elapsed.ToString();
            Console.WriteLine("time=" + timeStr);
            Console.WriteLine(list[0].Id);
            Console.WriteLine();


            stopWatch.Restart();
            // linq排序
            list1 = list1.OrderBy(x => x.age).ToList();
            stopWatch.Stop();
            string timeStr1 = stopWatch.Elapsed.ToString();
            Console.WriteLine("time=" + timeStr1);
            Console.WriteLine(list1[0].Id);

            #endregion
            Console.ReadKey();
        }
        private static string getMicrosecond() { long _ticks = 0; QueryPerformanceCounter(ref _ticks); Random R = new Random(); return Convert.ToString(_ticks) + R.Next(0, 100); }
        [DllImport("kernel32.dll")]
        extern static short QueryPerformanceCounter(ref long x);
        [DllImport("kernel32.dll")]
        extern static short QueryPerformanceFrequency(ref long x);

    }

}
