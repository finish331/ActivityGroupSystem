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
    public class DatabaseSystemTests
    {
        [TestMethod()]
        public void CreateDatabaseSystemTest()
        {
            Assert.IsNotNull(new DatabaseSystem());
        }

        [TestMethod]
        public async Task InitializationMemberDataTest()
        {
            DatabaseSystem databaseSystem = new DatabaseSystem();
            List<Member> memberList = await databaseSystem.InitializationMemberData();
            Assert.AreEqual("asd1234",memberList[0].MemberId);
        }

        [TestMethod]
        public async Task InitializationActivityDataTest()
        {
            DatabaseSystem databaseSystem = new DatabaseSystem();
            List<Activity> activityList = await databaseSystem.InitializationActivityData();
            Assert.AreEqual("3", activityList[0].ActivityId);
        }

        [TestMethod]
        public async Task InsertActivityTest()
        {
            DatabaseSystem databaseSystem = new DatabaseSystem();
            Activity activity = new Activity();
            activity.ActivityId = "10";
            activity.ActivityName = "unitTest2";
            activity.HomeOwnerId = "unitTest1";
            activity.ActivityDate = "2020/02/02";
            activity.ActivityNote = "unitTest";
            activity.NumberOfPeople = "3";

            Assert.IsTrue(await databaseSystem.InsertActivity(activity));
        }

        [TestMethod]
        public void UpdateMemberTest()
        {
            DatabaseSystem databaseSystem = new DatabaseSystem();
            Dictionary<string, string> testValue = new Dictionary<string, string>();
            testValue.Add("Phone", "0913265846");
            Assert.IsTrue(databaseSystem.UpdateMember("asd1234", testValue));
        }

        [TestMethod]
        public async Task UpdateActivityTest()
        {
            DatabaseSystem databaseSystem = new DatabaseSystem();
            Dictionary<string, string> testValue = new Dictionary<string, string>();

            testValue.Add("ActivityName", "unitTest2");
            testValue.Add("HomeOwnerId", "unitTest1");
            testValue.Add("ActivityDate", "1998/02/11");
            testValue.Add("ActivityNote", "unitTest");
            testValue.Add("NumberOfPeople", "3");

            Assert.IsTrue(await databaseSystem.UpdateActivity("unitTest", testValue));
        }

        [TestMethod]
        public async Task GetChatDataTest()
        {
            DatabaseSystem databaseSystem = new DatabaseSystem();
            List<Message> messages = new List<Message>();
            messages = await databaseSystem.GetChatData("3");
            Assert.AreEqual("member1", messages[0].MemberId);
        }

        [TestMethod]
        public async Task SendMessageTest()
        {
            DatabaseSystem databaseSystem = new DatabaseSystem();
            Message message = new Message("unitTest1","unitTest1","unitTestMessage");

            Assert.IsTrue(await databaseSystem.SendMessage("3", message));
        }
        
        [TestMethod]
        public async Task CheckAccountTest()
        {
            DatabaseSystem databaseSystem = new DatabaseSystem();

            Assert.IsTrue(await databaseSystem.CheckAccount("unitTest1", "test123"));
            Assert.IsFalse(await databaseSystem.CheckAccount("unitTest2", "test456"));
        }

        [TestMethod]
        public async Task InsertMemberTest()
        {
            DatabaseSystem databaseSystem = new DatabaseSystem();
            Dictionary<string, string> testValue = new Dictionary<string, string>();
            testValue.Add("MemberId", "unitTest4");
            testValue.Add("MemberName", "tom");
            testValue.Add("Password", "test123");
            testValue.Add("Phone", "0326598741");
            testValue.Add("Birthday", "10/10/1659");
            testValue.Add("Sexuality", "女");

            Assert.IsTrue(await databaseSystem.InsertMember(testValue));
        }

        [TestMethod]
        public async Task UpdateMemberInfoTest()
        {
            DatabaseSystem databaseSystem = new DatabaseSystem();
            Member member = new Member();
            member.MemberId = "unitTest4";
            member.MemberId = "unitTest4";
            member.MemberName = "zxczxc";
            member.Password = "test123";
            member.Phone = "0326598741";
            member.Birthday = "10/10/1659";
            member.Sexuality = "女";

            Assert.IsTrue(await databaseSystem.UpdateMemberInfo(member));
        }
    }
}