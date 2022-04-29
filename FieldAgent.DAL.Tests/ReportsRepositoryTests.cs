using FieldAgent.Core;
using FieldAgent.Core.DTOs;
using FieldAgent.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FieldAgent.DAL.Tests
{
    public class ReportsRepositoryTests
    {
        ReportsRepository db;
        DbFactory dbf;

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DbFactory(cp.Config, FactoryMode.TEST);
            db = new ReportsRepository(cp.Config);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void GetTopAgents_ReturnTopAgents()
        {
            Response<List<TopAgentListItem>> result = db.GetTopAgents();
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Rowe, Daryl", result.Data[0].NameLastFirst);
            Assert.AreEqual(new DateTime(1973,03,14), result.Data[0].DateOfBirth);
            Assert.AreEqual(1, result.Data[0].CompletedMissionCount);
        }

        [Test]
        public void GetPensionList_ReturnPensionList()
        {
            Response<List<PensionListItem>> result = db.GetPensionList(2);
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Monkhouse", result.Data[0].AgencyName);
            Assert.AreEqual(Guid.Parse("05814c1b-2d96-4584-897f-7eba9c2caf3d"), result.Data[0].BadgeId);
            Assert.AreEqual("Challes, Netta", result.Data[0].NameLastFirst);
            Assert.AreEqual(new DateTime(1988, 10, 18), result.Data[0].DateOfBirth);
            Assert.AreEqual(new DateTime(2013, 2, 28), result.Data[0].DeactivationDate);
        }

        [Test]
        public void GetAuditClearance_ReturnAuditClearance()
        {
            Response<List<ClearanceAuditListItem>> result = db.AuditClearance(2,2);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(Guid.Parse("05814c1b-2d96-4584-897f-7eba9c2caf3d"), result.Data[0].BadgeId);
            Assert.AreEqual("Challes, Netta", result.Data[0].NameLastFirst);
            Assert.AreEqual(new DateTime(1988, 10, 18), result.Data[0].DateOfBirth);
            Assert.AreEqual(new DateTime(2007, 4, 9), result.Data[0].ActivationDate);
            Assert.AreEqual(new DateTime(2013, 2, 28), result.Data[0].DeactivationDate);
        }
    }
}
