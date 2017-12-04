using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace UnitTest
{
    /*
     * 温故而知新
     */
    [TestClass]
    public class UnitTest1
    {
        #region 序列化 反序列化
        [TestMethod]
        public void TestMethod1()
        {
            var objectGraph = new List<String> { "a", "b", "c", "d", "e", "f" };
            Stream stream = SerializableToMemory(objectGraph);

            stream.Position = 0;
            objectGraph = null;

            objectGraph = (List<String>)DeserializeFromMemory(stream);
            foreach (var s in objectGraph)
                Console.WriteLine(s);
        }


        static MemoryStream SerializableToMemory(Object objectGraph)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, objectGraph);
            return stream;
        }
        static Object DeserializeFromMemory(Stream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }
        //用序列化获取对象的深拷贝
        static Object DeepClone(Object objectGraph)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Context = new System.Runtime.Serialization.StreamingContext(System.Runtime.Serialization.StreamingContextStates.Clone);
                formatter.Serialize(stream, objectGraph);
                stream.Position = 0;
                // 返回对象（深拷贝）的根
                return formatter.Deserialize(stream);
            }
        }
        #endregion


        #region appDomains
        [TestMethod]
        public void appDomains()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;

            // InstantiateMyTypeFail(currentDomain);

            // currentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);

            InstantiateMyTypeFail(currentDomain);

            InstantiateMyTypeSucceed(currentDomain);
        }
        private static void InstantiateMyTypeFail(AppDomain domain)
        {
            try
            {
                // You must supply a valid fully qualified assembly name here.
                domain.CreateInstance("Assembly text name, Version, Culture, PublicKeyToken", "UnitTest.MyType");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
        }

        private static void InstantiateMyTypeSucceed(AppDomain domain)
        {
            try
            {
                string asmname = Assembly.GetCallingAssembly().FullName;
                domain.CreateInstance(asmname, "UnitTest.MyType");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
        }

        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Console.WriteLine("Resolving...");
            return typeof(MyType).Assembly;
        }
        public class MyType
        {
            public MyType()
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("MyType 实例化!");
                Console.WriteLine("-----------------");
            }
        }

        #endregion

      
      

    }
     











     



}
