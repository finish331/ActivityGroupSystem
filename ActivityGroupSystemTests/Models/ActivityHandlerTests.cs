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
    public class ActivityHandlerTests
    {
        ActivityHandler _activityHandler;
        public void Initialization()
        {
            _activityHandler = new ActivityHandler();
            Activity test = new Activity()
            {
                ActivityId = "1",
                ActivityName = "test",
                ActivityNote = "This is a test",
                ActivityDate = "2020/05/12",
                NumberOfPeople = "10",
                ParticipantList = new List<string>() { "tester1", "tester2" },
                HomeOwnerId = "tester1"
            };

            _activityHandler.ActivityList.Add(test);

            Activity test2 = new Activity()
            {
                ActivityId = "2",
                ActivityName = "test2",
                ActivityNote = "This is a test2",
                ActivityDate = "2020/05/15",
                NumberOfPeople = "20",
                ParticipantList = new List<string>() { "tester2" },
                HomeOwnerId = "tester2"
            };
            _activityHandler.ActivityList.Add(test2);
        }
        [TestMethod()]
        public void FindActivityTest()
        {
            Initialization();

            Activity testActivity = _activityHandler.FindActivity("1");
            Assert.AreEqual("test", testActivity.ActivityName);

            testActivity = _activityHandler.FindActivity("30");
            Assert.AreSame(null, testActivity);

        }

        [TestMethod()]
        public void EnterJoinedActivityTest()
        {
            Initialization();

            Activity testActivity = _activityHandler.EnterJoinedActivity("tester1", "1");
            Assert.AreEqual("test", testActivity.ActivityName);

            testActivity = _activityHandler.EnterJoinedActivity("30", "30");
            Assert.AreSame(null, testActivity);
        }

        [TestMethod()]
        public void GetManageActivityTest()
        {
            Initialization();
            List<Activity> activities = new List<Activity>();
            activities = _activityHandler.GetManageActivity("tester1");
            Assert.AreEqual("test", activities[0].ActivityName);

            activities = _activityHandler.GetManageActivity("30");
            Assert.AreEqual(0, activities.Count);
        }

        [TestMethod()]
        public void GetUnJoinActivityTest()
        {
            Initialization();
            List<Activity> activities = new List<Activity>();
            activities = _activityHandler.GetUnJoinActivity("tester1");
            Assert.AreEqual("test2", activities[0].ActivityName);
            Assert.AreEqual(1, activities.Count);

            activities = _activityHandler.GetManageActivity("30");
            Assert.AreEqual(0, activities.Count);
        }

        [TestMethod()]
        public void GetJoinActivityTest()
        {
            Initialization();
            List<Activity> activities = new List<Activity>();
            activities = _activityHandler.GetJoinActivity("tester1");
            Assert.AreEqual("test", activities[0].ActivityName);
            Assert.AreEqual(1, activities.Count);

            activities = _activityHandler.GetManageActivity("30");
            Assert.AreEqual(0, activities.Count);
        }

        [TestMethod()]
        public void InputAcivityKeyWordTest()
        {
            Initialization();
            List<Activity> activities = new List<Activity>();
            activities = _activityHandler.InputAcivityKeyWord("1");
            Assert.AreEqual(1, activities.Count);
            Assert.AreEqual("1", activities[0].ActivityId);

            activities = _activityHandler.InputAcivityKeyWord("test");
            Assert.AreEqual(2, activities.Count);
            Assert.AreEqual("test2", activities[1].ActivityName);
        }

        [TestMethod()]
        public void KickOutPariticipantTest()
        {
            Initialization();
            bool result;
            //正確踢人
            result = _activityHandler.KickOutPariticipant("tester1", "1");
            Assert.IsTrue(result);
            //找到房間，但房間內沒有這名成員
            result = _activityHandler.KickOutPariticipant("tester1", "2");
            Assert.IsFalse(result);
            //錯誤的房間，錯誤的成員
            result = _activityHandler.KickOutPariticipant("tester1", "30");
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CreateActivityTest()
        {
            Initialization();
            Activity activity = new Activity()
            {
                ActivityName = "test3",
                ActivityNote = "This is a test3",
                ActivityDate = "2020/05/25",
                NumberOfPeople = "25",
                ParticipantList = new List<string>(),
                HomeOwnerId = "tester3"
            };
            _activityHandler.CreateActivity(activity);
            Assert.AreEqual(3, _activityHandler.ActivityList.Count);
            Assert.AreEqual("3", _activityHandler.ActivityList[2].ActivityId);
        }

        [TestMethod()]
        public void GetAllParticipantsTest()
        {
            Initialization();
            List<string> participant = new List<string>();
            participant = _activityHandler.GetAllParticipants("1");
            Assert.AreEqual(2, participant.Count);
            Assert.AreEqual("tester2", participant[1]);

            participant = _activityHandler.GetAllParticipants("2");
            Assert.AreEqual(1, participant.Count);
            Assert.AreEqual("tester2", participant[0]);

            participant = _activityHandler.GetAllParticipants("3");
            Assert.AreSame(null, participant);
        }

        [TestMethod()]
        public void TransferHomeownerTest()
        {
            Initialization();
            bool result;
            result = _activityHandler.TransferHomeowner("1", "tester2");
            Assert.IsTrue(result);
            Assert.AreEqual("tester2", _activityHandler.ActivityList[0].HomeOwnerId);

            //房間不存在
            result = _activityHandler.TransferHomeowner("3", "testtest");
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void LeaveActivityTest()
        {
            Initialization();
            bool result;
            result = _activityHandler.LeaveActivity("1", "tester2");
            Assert.IsTrue(result);
            Assert.AreEqual(1, _activityHandler.ActivityList[0].ParticipantList.Count);
            Assert.AreEqual("tester1", _activityHandler.ActivityList[0].ParticipantList[0]);

            //房間不存在
            result = _activityHandler.TransferHomeowner("3", "testtest");
            Assert.IsFalse(result);

        }

        [TestMethod()]
        public void JoinActivityTest()
        {
            Initialization();

            bool result = _activityHandler.JoinActivity("testJoin", "1");
            Assert.IsTrue(result);
            Assert.AreEqual(3, _activityHandler.ActivityList[0].ParticipantList.Count);
            Assert.AreEqual("testJoin", _activityHandler.ActivityList[0].ParticipantList[2]);

            //沒有ID為3的房間，加入失敗維持原本人數
            result = _activityHandler.JoinActivity("testJoin", "3");
            Assert.IsFalse(result);
            Assert.AreEqual(3, _activityHandler.ActivityList[0].ParticipantList.Count);
            Assert.AreEqual("tester1", _activityHandler.ActivityList[0].ParticipantList[0]);
        }

        [TestMethod()]
        public void SaveActivityTest()
        {
            Initialization();
            Dictionary<string, string> myDic = new Dictionary<string, string>()
            {
                { "name", "newTest" },
                { "ownerId", "newTester" },
                { "people", "10" },
                { "note", "Test Test" },
                { "date", "2020/05/12" }
            };
            bool result;
            result = _activityHandler.SaveActivity("1", myDic);
            Assert.IsTrue(result);
            Assert.AreEqual("newTest", _activityHandler.ActivityList[0].ActivityName);

            result = _activityHandler.SaveActivity("3", myDic);
            Assert.IsFalse(result);
        }
    }
}