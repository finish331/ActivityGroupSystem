using Microsoft.VisualStudio.TestTools.UnitTesting;
using ActivityGroupSystem.Controllers;
using ActivityGroupSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ActivityGroupSystem.Controllers.Tests
{
    [TestClass()]
    public class HallControllerTests
    {
        private HallController hall;

        [TestInitialize]
        public void Init()
        {
            hall = new HallController();
        }

        [TestMethod()]
        public void IndexTest()
        {
            var result = hall.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetAllActivityTest()
        {
            var result = hall.GetAllActivity().Result as JsonResult;
            Assert.IsNotNull(result);

            dynamic jsonCollection = result.Data;
            foreach (dynamic json in jsonCollection)
            {
                Assert.IsNotNull(json.ActivityId);
                Assert.IsNotNull(json.ActivityName);
                Assert.IsNotNull(json.ActivityDate);
                Assert.IsNotNull(json.ActivityNote);
                Assert.IsNotNull(json.NumberOfPeople);
                Assert.IsNotNull(json.HomeOwnerId);
                Assert.IsNotNull(json.ParticipantList);
            }
        }

        [TestMethod()]
        public void GetUnJoinActivityTest()
        {
            var result = hall.GetUnJoinActivity("member1").Result as JsonResult;
            Assert.IsNotNull(result);

            dynamic jsonCollection = result.Data;
            foreach (dynamic json in jsonCollection)
            {
                Assert.IsNotNull(json.ActivityId);
                Assert.IsNotNull(json.ActivityName);
                Assert.IsNotNull(json.ActivityDate);
                Assert.IsNotNull(json.ActivityNote);
                Assert.IsNotNull(json.NumberOfPeople);
                Assert.IsNotNull(json.HomeOwnerId);
                Assert.IsNotNull(json.ParticipantList);
            }
        }

        [TestMethod()]
        public void GetManageActivityTest()
        {
            var result = hall.GetManageActivity("member1").Result as JsonResult;
            Assert.IsNotNull(result);

            dynamic jsonCollection = result.Data;
            foreach (dynamic json in jsonCollection)
            {
                Assert.IsNotNull(json.ActivityId);
                Assert.IsNotNull(json.ActivityName);
                Assert.IsNotNull(json.ActivityDate);
                Assert.IsNotNull(json.ActivityNote);
                Assert.IsNotNull(json.NumberOfPeople);
                Assert.IsNotNull(json.HomeOwnerId);
                Assert.IsNotNull(json.ParticipantList);
            }
        }

        [TestMethod()]
        public void GetJoinActivityTest()
        {
            var result = hall.GetJoinActivity("member1").Result as JsonResult;
            Assert.IsNotNull(result);

            dynamic jsonCollection = result.Data;
            foreach (dynamic json in jsonCollection)
            {
                Assert.IsNotNull(json.ActivityId);
                Assert.IsNotNull(json.ActivityName);
                Assert.IsNotNull(json.ActivityDate);
                Assert.IsNotNull(json.ActivityNote);
                Assert.IsNotNull(json.NumberOfPeople);
                Assert.IsNotNull(json.HomeOwnerId);
                Assert.IsNotNull(json.ParticipantList);
            }
        }

        /*[TestMethod()]
        public void BlackMemberTest()
        {
            Assert.Fail();
        }*/

        [TestMethod()]
        public void InputAcivityKeyWordTest()
        {
            var result = hall.GetJoinActivity("asd").Result as JsonResult;
            Assert.IsNotNull(result);
        }

        /*[TestMethod()]
        public void AddFriendTest()
        {
            Assert.Fail();
        }*/

        /*[TestMethod()]
        public void CreateActivityTest()
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "name" , "sports" },
                { "ownerId" , "1" },
                { "people" , "10" },
                { "note" , "go to move" },
                { "date" , "2020/5/12" },
            };

            Activity activity = new Activity();
            activity.UpdateActivity(data);

            var result = hall.CreateActivity(activity, "1").Result as JsonResult;
            Assert.IsTrue((bool)result.Data);
        }*/

        /*[TestMethod()]
        public void RoomTest()
        {
            Assert.Fail();
        }*/

        /*[TestMethod()]
        public void updateActivityTest()
        {
            Dictionary<string, string> data = new Dictionary<string, string>
            {
                { "name" , "TTP" },
                { "ownerId" , "1" },
                { "people" , "10" },
                { "note" , "go go go" },
                { "date" , "2020/5/12" },
            };

            Activity activity = new Activity();
            activity.UpdateActivity(data);

            var create = hall.CreateActivity(activity, "1").Result as JsonResult;
            var result = hall.updateActivity("1", "play", "8", "let's play", "2020/05/13") as JsonResult;
        }*/

        /*[TestMethod()]
        public void transferHomeownerTest()
        {
            var result = hall.TransferHomeowner("1", "2") as JsonResult;
        }

        [TestMethod()]
        public void KickOutPariticipantTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InviteFriendTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LeaveActivityTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SendMessageTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void updateChatroomTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateNewMemberTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void JoinActivityTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SaveActivityTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateFriendListTest()
        {
            //Assert.Fail();
        }

        [TestMethod()]
        public void UpdateBlackListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateFriendInvitationTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RejectInvitationTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AgreeInvitationTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LogoutTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LoginTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LoginTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RegisterTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RegistersTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateMemberInfoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteFriendTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteBlackTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MemberInfoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void OtherMemberInfoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetFriendListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetBlackListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetInvitationListTest()
        {
            //Assert.Fail();
        }*/
    }
}