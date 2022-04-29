using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FieldAgent.DAL.Tests
{
    public class AliasRepositoryTests
    {
        AliasRepository db;
        DbFactory dbf;

        Alias expectedAlias = new Alias
        {
            AliasId = 1,
            AliasName = "ylogsdale0",
            InterpolId = Guid.Parse("745a1b47-a3e5-464d-93ec-3fff806c07b2"),
            Persona = "Serviceberry",
            AgentId = 1
        };
        Alias updatedAlias = new Alias
        {
            AliasId = 1,
            AliasName = "randomAliasName",
            InterpolId = Guid.Parse("745a1b47-a3e5-464d-93ec-3fff806c07b2"),
            Persona = "UpdatedServiceberry",
            AgentId = 1
        };
        Alias newAlias = new Alias
        {
            AliasName = "newAliasName",
            InterpolId = Guid.Parse("123a1b47-a3e5-464d-93ec-3fff806c07b2"),
            Persona = "newServiceberry",
            AgentId = 1
        };

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DbFactory(cp.Config, FactoryMode.TEST);
            db = new AliasRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void Get_GivenAliasId_ReturnAlias()
        {
            Response<Alias> result = db.Get(1);
            Assert.True(result.Success);
            Assert.AreEqual(result.Data, expectedAlias);
        }

        [Test]
        public void Delete_GivenAliasId_DeleteAlias()
        {
            Response result = db.Delete(1);
            Assert.True(result.Success);
            Assert.False(db.Get(1).Success);
        }

        [Test]
        public void Update_GivenAlias_UpdateAlias()
        {
            Response result = db.Update(updatedAlias);
            Assert.True(result.Success);

            Alias actual = db.Get(1).Data;
            Assert.AreEqual(updatedAlias,actual);
        }
        [Test]
        public void Insert_GivenAlias_InsertAlias()
        {
            Response<Alias> actual = db.Insert(newAlias);

            Assert.True(actual.Success);
            Assert.AreEqual(newAlias, actual.Data);
        }

        [Test]
        public void GetByAgent_GivenAgentId_ReturnAliases()
        {
            List<Alias> expected = new List<Alias>();
            expected.Add(expectedAlias);

            Response<List<Alias>> result = db.GetByAgent(1);

            Assert.True(result.Success);
            Assert.AreEqual(result.Data, expected);
        }
    }
}
