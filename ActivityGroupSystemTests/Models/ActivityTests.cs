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
    public class ActivityTests
    {
        Activity _activity;

        [TestInitialize]
        public void Initialization()
        {
            _activity = new Activity()
            {
                ActivityId = "1",
                ActivityName = "test",
                ActivityNote = "This is a test",
                ActivityDate = "2020/05/12",
                NumberOfPeople = "10",
                ParticipantList = new List<string>() { "tester1", "tester2" },
                HomeOwnerId = "tester1"
            };
        }

        [TestMethod()]
        public void IsMemberInActivityTest()
        {
            bool result;
            result = _activity.IsMemberInActivity("tester1");
            Assert.IsTrue(result);
            result = _activity.IsMemberInActivity("tester33");
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void KickOutPariticipantTest()
        {
            int result = _activity.KickOutPariticipant("tester1");
            Assert.AreEqual(result, 0);
            Assert.AreEqual(1, _activity.ParticipantList.Count);

            result = _activity.KickOutPariticipant("tester1");
            Assert.AreEqual(result, 1);
            Assert.AreEqual(1, _activity.ParticipantList.Count);
        }

        [TestMethod()]
        public void TransferHomeownerTest()
        {

            Assert.AreEqual("tester1", _activity.HomeOwnerId);
            _activity.TransferHomeowner("tester3");
            Assert.AreEqual("tester3", _activity.HomeOwnerId);
        }

        [TestMethod()]
        public void LeaveTest()
        {

            Assert.AreEqual(2, _activity.ParticipantList.Count);
            _activity.Leave("tester1");
            Assert.AreEqual(1, _activity.ParticipantList.Count);
        }

        [TestMethod()]
        public void AddNewParticipantTest()
        {
            bool result = _activity.AddNewParticipant("tester3");
            Assert.IsTrue(result);
            Assert.AreEqual("tester3", _activity.ParticipantList[2]);

            result = _activity.AddNewParticipant("tester3");
            Assert.IsFalse(result);
            Assert.AreEqual("tester2", _activity.ParticipantList[1]);
        }

        [TestMethod()]
        public void IsExistTest()
        {
            Assert.IsTrue(_activity.IsExist("1"));
        }

        [TestMethod()]
        public void UpdateActivityTest()
        {
            Dictionary<string, string> myDic = new Dictionary<string, string>()
            {
                { "name", "newTest" },
                { "ownerId", "newTester" },
                { "people", "10" },
                { "note", "Test Test" },
                { "date", "2020/05/12" }
            };

            Dictionary<string, string> errorDic = new Dictionary<string, string>()
            {
                { "name", "newTest" },
                { "ownerI", "newTester" },
                { "people", "10" }
            };
            bool result,result2;
            result = _activity.UpdateActivity(myDic);
            result2= _activity.UpdateActivity(errorDic);
            Assert.IsTrue(result);
            Assert.IsFalse(result2);
            Assert.AreEqual("newTest", _activity.ActivityName);
        }

        [TestMethod()]
        public void InitializeChatroomTest()
        {
            List<Message> message = new List<Message>();
            _activity.InitializeChatroom(message);

            Assert.AreEqual(_activity.GetChatData().Count, message.Count());
        }

        [TestMethod()]
        public void IsParticipantTest()
        {
            List<Message> message = new List<Message>();
            _activity.InitializeChatroom(message);

            Assert.AreEqual(_activity.GetChatData().Count, message.Count());
        }
    }
}