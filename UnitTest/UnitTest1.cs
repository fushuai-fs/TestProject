using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var objectGraph = new List<String> { "a","b","c","d","e","f"};
            Stream stream = SerializableToMemory(objectGraph);

            stream.Position = 0;
            objectGraph = null;

            objectGraph =(List<String>)DeserializeFromMemory(stream);
            foreach (var s in objectGraph)
                Console.WriteLine(s);

        }
        static MemoryStream SerializableToMemory(Object objectGraph)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream,objectGraph);
            return stream;
        }
        static Object DeserializeFromMemory(Stream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }

    }
}
