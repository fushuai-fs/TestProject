
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            // MongodbTest.test();
            //  new autoMapperTest(); 

            //AccessingObjectsAcrossAppDomainBoundaries.Marshalling();
            //AccessingObjectsAcrossAppDomainBoundaries.Marshalling2();
            //AccessingObjectsAcrossAppDomainBoundaries.Marshalling3();

            using (new AppDomainMonitorDelta(null))
            {
                // Allocate about 10 million bytes that will survive collections
                var list = new List<Object>();
                for (Int32 x = 0; x < 1000; x++) list.Add(new Byte[10000]);
                // Allocate about 20 million bytes that will NOT survive collections
                for (Int32 x = 0; x < 2000; x++) new Byte[10000].GetType();
                // Spin the CPU for about 5 seconds
                Int64 stop = Environment.TickCount + 5000;
                while (Environment.TickCount < stop) ;
            }

            Console.ReadKey();
        }

    }






}
