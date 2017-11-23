
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
             
            AccessingObjectsAcrossAppDomainBoundaries.Marshalling3();

            Console.ReadKey();
        }

    }






}
