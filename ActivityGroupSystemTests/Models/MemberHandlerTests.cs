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
    public class MemberHandlerTests
    {



        private List<Member> GetTestMemberList()
        {
            List<Member> testMemberList = new List<Member>();
            testMemberList.Add(new Member
            {
                MemberId = "asd1",
                MemberName = "Demo1",
                Password = "asd123",
                Phone = "0949872965",
                Sexuality = "男",
                Birthday = "29/02/1998",
                BlackList = new List<string> { "asd2" },
                FriendList = new List<string> { "asd3" },
                FriendInvitation = new List<string> { "asd4" }
            });
            testMemberList.Add(new Member
            {
                MemberId = "asd2",
                MemberName = "Ash",
                Password = "asd123",
                Phone = "0949872965",
                Sexuality = "男",
                Birthday = "29/02/1998",
                BlackList = new List<string>(),
                FriendList = new List<string> { "asd3" },
                FriendInvitation = new List<string> { "asd4" }
            });
            testMemberList.Add(new Member
            {
                MemberId = "asd3",
                MemberName = "Seth",
                Password = "asd123",
                Phone = "0949872965",
                Sexuality = "男",
                Birthday = "29/02/1998",
                BlackList = new List<string>(),
                FriendList = new List<string> { "asd1" },
                FriendInvitation = new List<string>()
            });
            testMemberList.Add(new Member
            {
                MemberId = "asd4",
                MemberName = "Vincent",
                Password = "asd123",
                Phone = "0949872965",
                Sexuality = "男",
                Birthday = "29/02/1998",
                BlackList = new List<string> { "asd2" },
                FriendList = new List<string>(),
                FriendInvitation = new List<string> { "asd3" }
            });
            testMemberList.Add(new Member
            {
                MemberId = "asd5",
                MemberName = "Edison",
                Password = "asd123",
                Phone = "0949872965",
                Sexuality = "男",
                Birthday = "29/02/1998",
                BlackList = new List<string>(),
                FriendList = new List<string>(),
                FriendInvitation = new List<string>()
            });
            return testMemberList;
        }

        [TestMethod()]
        public void CreateMemberHandlerTest()
        {
            MemberHandler testMemberHandler = new MemberHandler(GetTestMemberList());
            Assert.IsNotNull(testMemberHandler);
            Assert.IsNotNull(new MemberHandler(null));
        }

        [TestMethod]
        public void GetBlackListTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Assert.AreEqual("asd2", memberHandler.GetBlackList("asd1")[0]);
        }

        [TestMethod()]
        public void BlackMemberTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Assert.IsTrue(memberHandler.BlackMember("asd5", "asd1"));
            Assert.IsFalse(memberHandler.BlackMember("zxc1", "asd1"));
        }

        [TestMethod()]
        public void AddFriendInvitationTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Assert.IsTrue(memberHandler.AddFriendInvitation("asd5", "asd1"));
            Assert.IsFalse(memberHandler.AddFriendInvitation("asd5", "zxc1"));
        }

        [TestMethod()]
        public void UpdateUserDataTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Dictionary<string, string> testData = new Dictionary<string, string>();
            testData.Add("MemberId", "zxc1");
            testData.Add("Password", "zxc123");
            testData.Add("MemberName", "John");
            testData.Add("Sexuality", "女");
            testData.Add("Phone", "0321654987");
            testData.Add("Birthday", "10/10/1999");
            Assert.IsTrue(memberHandler.UpdateUserData("asd5", testData));
            Assert.IsFalse(memberHandler.UpdateUserData("zxc2", testData));
        }

        [TestMethod()]
        public void GetFirendListTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Assert.AreEqual("asd3", memberHandler.GetFriendsList("asd1")[0]);
            Assert.IsNull(memberHandler.GetFriendsList("zxc1"));
        }

        [TestMethod()]
        public void InviteMemberTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Assert.IsTrue(memberHandler.InviteMember("asd6", "asd1", "1"));
            Assert.IsFalse(memberHandler.InviteMember("asd6", "zxc1", "1"));
        }

        [TestMethod()]
        public void DeleteFriendTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Assert.IsFalse(memberHandler.DeleteFriend("asd1", "asd2"));
            Assert.IsFalse(memberHandler.DeleteFriend("zxc1", "asd3"));
            Assert.IsTrue(memberHandler.DeleteFriend("asd1", "asd3"));
        }

        [TestMethod()]
        public void GetMemberByIdTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Assert.AreEqual("Demo1", memberHandler.GetMemberById("asd1").MemberName);
            Assert.IsNull(memberHandler.GetMemberById("zxc1"));
        }

        [TestMethod()]
        public void CreateNewMemberTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Dictionary<string, string> testData = new Dictionary<string, string>();
            testData.Add("MemberId", "zxc1");
            testData.Add("Password", "zxc123");
            testData.Add("MemberName", "zxc1");
            testData.Add("Sexuality", "女");
            testData.Add("Phone", "0321654987");
            testData.Add("Birthday", "10/10/1999");
            Assert.IsTrue(memberHandler.CreateNewMember(testData));
            testData["MemberId"] = "asd1";
            Assert.IsFalse(memberHandler.CreateNewMember(testData));
        }

        [TestMethod()]
        public void AgreeInvitationTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Assert.IsTrue(memberHandler.AgreeInvitation("asd1", "asd4"));
        }

        [TestMethod()]
        public void RejectInvitationTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Assert.IsTrue(memberHandler.RejectInvitation("asd1", "asd4"));
            Assert.IsFalse(memberHandler.RejectInvitation("zxc1", "asd4"));
        }

        [TestMethod()]
        public void GetInvitationListTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Assert.AreEqual("asd4", memberHandler.GetInvitationList("asd1")[0]);
            Assert.IsNull(memberHandler.GetInvitationList("zxc1"));
        }

        [TestMethod()]
        public void DeleteBlackTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Assert.IsTrue(memberHandler.DeleteBlack("asd1", "asd2"));
            Assert.IsFalse(memberHandler.DeleteBlack("zxc1", "asd2"));
        }

        [TestMethod()]
        public void IsBlackTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Assert.IsTrue(memberHandler.IsBlack("asd1","asd2"));
            Assert.IsFalse(memberHandler.IsBlack("asd1","asd3"));
        }

        [TestMethod()]
        public void CheckLoginAccountTest()
        {
            MemberHandler memberHandler = new MemberHandler(GetTestMemberList());
            Assert.AreEqual(memberHandler.CheckLoginAccount("asd1","asd123"),"");
            Assert.AreEqual(memberHandler.CheckLoginAccount("asd1", "asd1"), "密碼輸入錯誤");
            Assert.AreEqual(memberHandler.CheckLoginAccount("asd6", "asd1"), "帳號尚未註冊");
        }
    }
}