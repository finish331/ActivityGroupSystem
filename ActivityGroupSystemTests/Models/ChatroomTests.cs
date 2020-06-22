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
    public class ChatroomTests
    {
        Chatroom _chatroom;

        [TestInitialize]
        public void Initialization()
        {
            _chatroom = new Chatroom();
        }

        [TestMethod()]
        public void SendMessageTest()
        {
            _chatroom.SendMessage("1", "seth", "hello");
            Assert.AreEqual(_chatroom.MessageList.Count, 1);
            Assert.AreEqual(_chatroom.MessageList[0].MemberId, "1");
            Assert.AreEqual(_chatroom.MessageList[0].MemberName, "seth");
            Assert.AreEqual(_chatroom.MessageList[0].MessageContent, "hello");

            _chatroom.SendMessage("2", "ting", "hi");
            Assert.AreEqual(_chatroom.MessageList.Count, 2);
            Assert.AreEqual(_chatroom.MessageList[1].MemberId, "2");
            Assert.AreEqual(_chatroom.MessageList[1].MemberName, "ting");
            Assert.AreEqual(_chatroom.MessageList[1].MessageContent, "hi");
        }

        [TestMethod()]
        public void InitializeTest()
        {
            List<Message> myMessages = new List<Message>();
            myMessages.Add(new Message("3", "willy", "nice to meet you"));
            myMessages.Add(new Message("4", "amy", "nice to meet you too"));

            _chatroom.Initialize(myMessages);
            Assert.AreEqual(_chatroom.MessageList, myMessages);
        }
    }
}