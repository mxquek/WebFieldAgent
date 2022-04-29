using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FieldAgent.DAL.Tests
{
    public class AgentRepositoryTests
    {

        AgentRepository db;
        DbFactory dbf;

        Agent expectedAgent = new Agent
        {
            AgentId = 1,
            FirstName = "Faydra",
            LastName = "Vamplers",
            DateOfBirth = new DateTime(1989, 3, 14),
            Height = 80.24M
        };
        Agent expectedUpdatedAgent = new Agent
        {
            AgentId = 1,
            FirstName = "UpdatedFaydra",
            LastName = "UpdatedVamplers",
            DateOfBirth = new DateTime(1999, 3, 14),
            Height = 70.24M
        };
        Agent expectedNewAgent = new Agent
        {
            FirstName = "New",
            LastName = "Agent",
            DateOfBirth = new DateTime(2000, 1, 1),
            Height = 90.24M
        };


        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DbFactory(cp.Config, FactoryMode.TEST);
            db = new AgentRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void Get_GivenAgentId_ReturnAgent()
        {
            Response <Agent> result = db.Get(1);
            Assert.True(result.Success);
            Assert.AreEqual(result.Data, expectedAgent);
        }

        [Test]
        public void GetMissions_GivenAgentId_ReturnMissions()
        {
            List<Mission> expected = new List<Mission>();
            expected.Add(MissionRepositoryTests.expectedMission);

            
            Response<List<Mission>> result = db.GetMissions(1);
            Assert.True(result.Success);
            Assert.AreEqual(result.Data, expected);
        }

        [Test]
        public void Delete_GivenAgentId_DeleteAgent()
        {
            Response result = db.Delete(1);
            Assert.True(result.Success);
            Assert.False(db.Get(1).Success);
        }

        [Test]
        public void Insert_GivenAgent_InsertAgent()
        {
            Response<Agent> actual = db.Insert(expectedNewAgent);

            Assert.True(actual.Success);
            Assert.AreEqual(expectedNewAgent, actual.Data);
        }

        [Test]
        public void Update_GivenAgent_UpdateAgent()
        {
            Response result = db.Update(expectedUpdatedAgent);
            Assert.True(result.Success);

            Agent actual = db.Get(1).Data;
            Assert.AreEqual(expectedUpdatedAgent, actual);
        }
    }
}