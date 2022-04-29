using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FieldAgent.DAL.Tests
{
    public class AgencyAgentRepositoryTests
    {
        AgencyAgentRepository db;
        DbFactory dbf;

        AgencyAgent expectedAgencyAgent = new AgencyAgent
        {
            AgencyId = 1,
            AgentId = 1,
            SecurityClearanceId = 1,
            BadgeId = Guid.Parse("0771ef06-cf1d-4429-a117-9795d27e6723"),
            ActivationDate = new DateTime(2000,1,4),
            DeactivationDate = new DateTime(2021,9,23),
            IsActive = true
        };

        AgencyAgent expectedUpdatedAgencyAgent = new AgencyAgent
        {
            AgencyId = 1,
            AgentId = 1,
            SecurityClearanceId = 2,
            BadgeId = Guid.Parse("1231ef06-cf1d-4429-a117-9795d27e6723"),
            ActivationDate = new DateTime(2000, 1, 1),
            DeactivationDate = new DateTime(2022, 12, 12),
            IsActive = true
        };

        AgencyAgent expectedNewAgencyAgent = new AgencyAgent
        {
            AgencyId = 1,
            AgentId = 2,
            SecurityClearanceId = 1,
            BadgeId = Guid.Parse("1231ef06-cf1d-4429-a117-9795d27e6723"),
            ActivationDate = new DateTime(2000, 1, 1),
            DeactivationDate = new DateTime(2021, 12, 25),
            IsActive = false
        };



        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DbFactory(cp.Config, FactoryMode.TEST);
            db = new AgencyAgentRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void Get_GivenAgencyAndAgentIds_ReturnAgencyAgent()
        {
            Response<AgencyAgent> actual = db.Get(1, 1);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Data, expectedAgencyAgent);
        }

        [Test]
        public void Insert_GivenAgencyAgent_InsertAgencyAgent()
        {
            Response<AgencyAgent> actual = db.Insert(expectedNewAgencyAgent);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Data, expectedNewAgencyAgent);
        }

        [Test]
        public void Delete_GivenAgencyAndAgentIds_DeleteAgencyAgent()
        {
            Response result = db.Delete(1, 1);

            Assert.IsTrue(result.Success);

            Assert.IsFalse(db.Get(1, 1).Success);
            Assert.AreEqual(db.Get(1,1).Data, null);
        }
        [Test]
        public void Update_GivenAgencyAgent_UpdateAgencyAgent()
        {
            Response result = db.Update(expectedUpdatedAgencyAgent);

            Assert.IsTrue(result.Success);

            AgencyAgent actual = db.Get(1, 1).Data;
            Assert.AreEqual(actual, expectedUpdatedAgencyAgent);
        }

        [Test]
        public void GetByAgency_GivenAgencyId_ReturnAgencyAgents()
        {
            List<AgencyAgent> expected = new List<AgencyAgent>();
            expected.Add(expectedAgencyAgent);
            expected.Add(expectedNewAgencyAgent);

            db.Insert(expectedNewAgencyAgent);
            Response<List<AgencyAgent>> actual = db.GetByAgency(1);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected, actual.Data);
        }

        [Test]
        public void GetByAgent_GivenAgentId_ReturnAgencyAgents()
        {
            List<AgencyAgent> expected = new List<AgencyAgent>();
            expected.Add(expectedAgencyAgent);

            Response<List<AgencyAgent>> actual = db.GetByAgent(1);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected, actual.Data);
        }
    }
}
