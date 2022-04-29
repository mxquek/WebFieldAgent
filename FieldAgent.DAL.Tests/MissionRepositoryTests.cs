using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FieldAgent.DAL.Tests
{
    public class MissionRepositoryTests
    {
        MissionRepository db;
        DbFactory dbf;

        public static Mission expectedMission = new Mission
        {
            MissionId = 1,
            CodeName = "Jordanna",
            StartDate = new DateTime(2019, 12, 13),
            ProjectedEndDate = new DateTime(2015, 3, 4),
            ActualEndDate = new DateTime(2016,9,22),
            OperationalCost = 2612.88M,
            Notes = null,

            AgencyId = 1
        };
        public static Mission expectedUpdatedMission = new Mission
        {
            MissionId = 1,
            CodeName = "UpdateJordanna",
            StartDate = new DateTime(2019, 12, 13),
            ProjectedEndDate = new DateTime(2015, 3, 4),
            ActualEndDate = new DateTime(2015, 9, 22),
            OperationalCost = 4612.88M,
            Notes = null,

            AgencyId = 3
        };
        public static Mission expectedNewMission = new Mission
        {
            CodeName = "NewMission",
            StartDate = new DateTime(2020, 12, 13),
            ProjectedEndDate = new DateTime(2021, 3, 4),
            ActualEndDate = new DateTime(2021, 9, 22),
            OperationalCost = 3612.88M,
            Notes = null,

            AgencyId = 2
        };

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DbFactory(cp.Config, FactoryMode.TEST);
            db = new MissionRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void GetByAgency_GivenAgencyId_ReturnMissions()
        {
            List<Mission> expected = new List<Mission>();
            expected.Add(expectedMission);

            Response<List<Mission>> actual = db.GetByAgency(1);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Data, expected);
        }

        [Test]
        public void Delete_GivenMissionId_DeleteMission()
        {
            Response actual = db.Delete(1);

            Assert.IsTrue(actual.Success);
            Assert.False(db.Get(1).Success);
        }
        [Test]
        public void Insert_GivenMission_InsertMission()
        {
            Response<Mission> actual = db.Insert(expectedNewMission);

            Assert.True(actual.Success);
            Assert.AreEqual(expectedNewMission, actual.Data);
        }

        [Test]
        public void Update_GivenMission_UpdateMission()
        {
            Response result = db.Update(expectedUpdatedMission);
            Assert.True(result.Success);

            Mission actual = db.Get(1).Data;
            Assert.AreEqual(expectedUpdatedMission, actual);
        }

        [Test]
        public void Get_GivenAgentId_ReturnAgent()
        {
            Response<Mission> result = db.Get(1);
            Assert.True(result.Success);
            Assert.AreEqual(result.Data, expectedMission);
        }

        [Test]
        public void GetByAgent_GivenAgentId_ReturnMissions()
        {
            List<Mission> expected = new List<Mission>();
            expected.Add(expectedMission);

            Response<List<Mission>> result = db.GetByAgent(1);
            Assert.True(result.Success);
            Assert.AreEqual(result.Data, expected);
        }
    }
}
