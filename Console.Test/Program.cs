
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.ReadKey();


            #region MyRegion

            // MongodbTest.test();
            //  new autoMapperTest(); 

            //AccessingObjectsAcrossAppDomainBoundaries.Marshalling();
            //AccessingObjectsAcrossAppDomainBoundaries.Marshalling2();
            //AccessingObjectsAcrossAppDomainBoundaries.Marshalling3();

            // AppDomain 监控
            //using (new AppDomainMonitorDelta(null))
            //{
            //    // Allocate about 10 million bytes that will survive collections
            //    var list = new List<Object>();
            //    for (Int32 x = 0; x < 1000; x++) list.Add(new Byte[10000]);
            //    // Allocate about 20 million bytes that will NOT survive collections
            //    for (Int32 x = 0; x < 2000; x++) new Byte[10000].GetType();
            //    // Spin the CPU for about 5 seconds
            //    Int64 stop = Environment.TickCount + 5000;
            //    while (Environment.TickCount < stop) ;
            //}

            ////  ReflectionD
            //            String dataAssembly = "System.Data, version=4.0.0.0, " +
            //"culture=neutral, PublicKeyToken=b77a5c561934e089";
            //            LoadAssemAndShowPublicTypes(dataAssembly);

            //            Console.WriteLine(Environment.NewLine);

            //            Go();

            //// 获取对泛型类型的类型对象的引用
            //Type openType = typeof(Dictionary<,>);
            //// 使用封闭泛型类型 TKey=String, TValue=Int32
            //Type closedType = openType.MakeGenericType(typeof(String), typeof(Int32));
            //// Construct an instance of the closed type
            //Object o = Activator.CreateInstance(closedType);
            //// 
            //Console.WriteLine(o.GetType());

            //closedType = openType.MakeGenericType(typeof(String), typeof(Boolean));
            //// Construct an instance of the closed type
            //  o = Activator.CreateInstance(closedType);

            //Console.WriteLine(o.GetType());

            #endregion

            Console.ReadKey();
        }




    }


    }
