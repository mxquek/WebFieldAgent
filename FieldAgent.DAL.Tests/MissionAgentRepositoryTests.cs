using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DAL.Tests
{
    public class MissionAgentRepositoryTests
    {
        MissionAgentRepository db;
        DbFactory dbf;

        MissionAgent expectedMissionAgent = new MissionAgent
        {
            MissionId = 1,
            AgentId = 1
        };

        MissionAgent expectedNewMissionAgent = new MissionAgent
        {
            MissionId = 1,
            AgentId = 3
        };

        MissionAgent expectedUpdatedMissionAgent = new MissionAgent
        {
            MissionId = 2,
            AgentId = 1
        };

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DbFactory(cp.Config, FactoryMode.TEST);
            db = new MissionAgentRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void Get_GivenMissionAndAgentIds_ReturnMissionAgent()
        {
            Response<MissionAgent> actual = db.Get(1, 1);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Data, expectedMissionAgent);
        }
        [Test]
        public void GetByMission_GivenMissionId_ReturnMissionAgents()
        {
            List<MissionAgent> expected = new List<MissionAgent>();
            expected.Add(expectedMissionAgent);
            expected.Add(expectedNewMissionAgent);

            db.Insert(expectedNewMissionAgent);
            Response<List<MissionAgent>> actual = db.GetByMission(1);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected, actual.Data);
        }
        [Test]
        public void GetByAgent_GivenAgentId_ReturnMissionAgents()
        {
            List<MissionAgent> expected = new List<MissionAgent>();
            expected.Add(expectedMissionAgent);
            expected.Add(expectedUpdatedMissionAgent);

            db.Insert(expectedUpdatedMissionAgent);
            Response<List<MissionAgent>> actual = db.GetByAgent(1);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected, actual.Data);
        }

        [Test]
        public void Insert_GivenMissionAgent_InsertMissionAgent()
        {
            Response<MissionAgent> actual = db.Insert(expectedNewMissionAgent);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Data, expectedNewMissionAgent);
        }
        [Test]
        public void Delete_GivenMissionAndAgentIds_DeleteMissionAgent()
        {
            Response result = db.Delete(1, 1);

            Assert.IsTrue(result.Success);

            Assert.IsFalse(db.Get(1, 1).Success);
            Assert.AreEqual(db.Get(1, 1).Data, null);
        }
    }
}
