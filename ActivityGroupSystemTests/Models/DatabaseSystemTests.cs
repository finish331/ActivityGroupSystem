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
        public async void InitializationMemberDataTest()
        {
             
            DatabaseSystem databaseSystem = new DatabaseSystem();
            List<Member> memberList = await databaseSystem.InitializationMemberData();
            Assert.AreEqual(8,memberList.Count);
        }
    }
}