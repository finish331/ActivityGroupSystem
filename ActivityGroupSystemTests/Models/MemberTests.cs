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
    public class MemberTests
    {
        private Member member, fillMember;

        [TestInitialize]
        public void Init()
        {
            Dictionary<string, string> memberInfo = new Dictionary<string, string>
            {
                { "MemberId", "test321" },
                { "MemberName", "test2" },
                { "Password", "123test" },
                { "Sexuality", "female" },
                { "Birthday", "321" },
                { "Phone", "0987654321" },
            };

            member = new Member();
            fillMember = new Member(memberInfo);
            fillMember.FriendList = new List<string>() { "1" };
            fillMember.BlackList = new List<string>() { "2" };
            fillMember.FriendInvitation = new List<string>() { "3" };
        }

        [TestMethod()]
        public void MemberTest_Empty()
        {
            Assert.AreEqual(member.MemberId, "");
            Assert.AreEqual(member.MemberName, "");
            Assert.AreEqual(member.Password, "");
            Assert.AreEqual(member.Sexuality, "");
            Assert.AreEqual(member.Birthday, "");
            Assert.AreEqual(member.Phone, "");
            Assert.AreEqual(member.FriendList.Count, 0);
            Assert.AreEqual(member.BlackList.Count, 0);
            Assert.AreEqual(member.InvitedList.Count, 0);
            Assert.AreEqual(member.FriendInvitation.Count, 0);
        }

        [TestMethod()]
        public void BlackMemberTest()
        {
            Assert.IsTrue(member.BlackMember("test"));
            Assert.IsFalse(member.BlackMember("test"));
        }

        [TestMethod()]
        public void AddFriendInvitationTest()
        {
            Assert.IsTrue(member.AddFriendInvitation("test"));
            Assert.IsFalse(member.AddFriendInvitation("test"));
        }

        [TestMethod()]
        public void UpdataDataTest()
        {
            Dictionary<string, string> newData = new Dictionary<string, string>
            {
                { "MemberId", "test123" },
                { "MemberName", "test" },
                { "Password", "321test" },
                { "Sexuality", "male" },
                { "Birthday", "123" },
                { "Phone", "0912345678" },
            };

            member.UpdataData(newData);
            Assert.AreEqual(member.MemberId, "test123");
            Assert.AreEqual(member.MemberName, "test");
            Assert.AreEqual(member.Password, "321test");
            Assert.AreEqual(member.Sexuality, "male");
            Assert.AreEqual(member.Birthday, "123");
            Assert.AreEqual(member.Phone, "0912345678");
        }

        [TestMethod()]
        public void InvitedTest()
        {
            // 第一次邀請
            member.Invited("test1", "1");
            Assert.AreEqual(member.InvitedList["1"], "test1");

            // 同一人重複邀請
            member.Invited("test1", "1");
            Assert.AreEqual(member.InvitedList["1"], "test1");

            // 不同人邀請
            member.Invited("test2", "1");
            Assert.AreEqual(member.InvitedList["1"], "test1,test2");
        }

        [TestMethod()]
        public void DeleteFriendTest()
        {
            member.InsertFriendList("test");
            Assert.IsTrue(member.DeleteFriend("test"));
            Assert.IsFalse(member.DeleteFriend("test"));
        }

        [TestMethod()]
        public void MemberTest_Fill()
        {
            Assert.AreEqual(fillMember.MemberId, "test321");
            Assert.AreEqual(fillMember.MemberName, "test2");
            Assert.AreEqual(fillMember.Password, "123test");
            Assert.AreEqual(fillMember.Sexuality, "female");
            Assert.AreEqual(fillMember.Birthday, "321");
            Assert.AreEqual(fillMember.Phone, "0987654321");
            Assert.AreEqual(fillMember.FriendList[0], "1");
            Assert.AreEqual(fillMember.BlackList[0], "2");
            Assert.AreEqual(fillMember.FriendInvitation[0], "3");
        }

        [TestMethod()]
        public void IsExistTest()
        {
            Assert.IsTrue(fillMember.IsExist("test321"));
        }

        [TestMethod()]
        public void InsertFriendListTest()
        {
            member.InsertFriendList("test456");
            Assert.AreEqual(member.FriendList[0], "test456");
        }

        [TestMethod()]
        public void DeleteFriendInvitationTest()
        {
            fillMember.DeleteFriendInvitation("3");
            Assert.AreEqual(fillMember.FriendInvitation.Count, 0);
        }

        [TestMethod()]
        public void IsBlackTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteBlackTest()
        {
            fillMember.DeleteBlack("2");
            Assert.AreEqual(fillMember.BlackList.Count, 0);
        }
    }
}