using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
   public class EmailTest
    {
        [TestMethod]
        public void emailtest()
        {
            POP3Mail popmail = new POP3Mail();
            popmail.Receive();


        }
    }
}
