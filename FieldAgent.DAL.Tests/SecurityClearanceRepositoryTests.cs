using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;

namespace FieldAgent.DAL.Tests
{
    public class SecurityClearanceRepositoryTests
    {
        SecurityClearanceRepository db;
        DbFactory dbf;

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DbFactory(cp.Config, FactoryMode.TEST);
            db = new SecurityClearanceRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void Get_GivenSecurityClearanceId_ReturnSecurityClearance()
        {
            Response<SecurityClearance> actual = new Response<SecurityClearance>();
            string expectedSecurityClearance = "None";

            actual = db.Get(1);

            Assert.AreEqual(actual.Data.SecurityClearanceName,expectedSecurityClearance);
        }

        [Test]
        public void GetAll_ReturnAllSecurityClearances()
        {
            Response<List<SecurityClearance>> actual = new Response<List<SecurityClearance>>();
            List<SecurityClearance> expected = new List<SecurityClearance>();
            expected.Add(new SecurityClearance { SecurityClearanceId = 1, SecurityClearanceName = "None"});
            expected.Add(new SecurityClearance { SecurityClearanceId = 2, SecurityClearanceName = "Retired" });
            expected.Add(new SecurityClearance { SecurityClearanceId = 3, SecurityClearanceName = "Secret" });
            expected.Add(new SecurityClearance { SecurityClearanceId = 4, SecurityClearanceName = "Top Secret" });
            expected.Add(new SecurityClearance { SecurityClearanceId = 5, SecurityClearanceName = "Black Ops" });

            actual = db.GetAll();

            Assert.AreEqual(actual.Data, expected);
        }
    }
}
