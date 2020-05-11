using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActivityGroupSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityGroupSystem.Models.Tests
{
    [TestClass()]
    public class MessageTests
    {
        [TestMethod()]
        public void TestMessage()
        {
            Message message = new Message("1", "tester", "testContent");

            Assert.AreEqual("1", message.MemberId);
            Assert.AreEqual("tester", message.MemberName);
            Assert.AreEqual("testContent", message.MessageContent);
        }
    }
}