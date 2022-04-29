using NUnit.Framework;
using System.Collections.Generic;
using FieldAgent.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using FieldAgent.Core.Entities;
using FieldAgent.Core;

namespace FieldAgent.DAL.Tests
{
    public class AgencyRepositoryTests
    {
        AgencyRepository db;
        DbFactory dbf;
        LocationRepository locationRepository;
        MissionRepository missionRepository;

        Agency expectedAgency = new Agency
        {
            AgencyId = 1,
            ShortName = "Gibbins",
            LongName = "Browsebug"
        };
        Agency expectedUpdatedAgency = new Agency
        {
            AgencyId = 1,
            ShortName = "UpdatedName",
            LongName = "UpdatedBrowsebug"
        };
        Agency expectedNewAgency = new Agency
        {
            ShortName = "NewName",
            LongName = "NewBrowsebug"
        };

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DbFactory(cp.Config, FactoryMode.TEST);
            locationRepository = new LocationRepository(dbf);
            missionRepository = new MissionRepository(dbf);
            db = new AgencyRepository(dbf, locationRepository, missionRepository);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void Get_GivenAgencyId_ReturnAgency()
        {
            Response<Agency> actual = db.Get(1);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Data, expectedAgency);
        }

        [Test]
        public void GetAll_ReturnAllAgencies()
        {
            List<Agency> expected = new List<Agency>();
            expected.Add(new Agency { AgencyId = 1, ShortName = "Gibbins", LongName = "Browsebug" });
            expected.Add(new Agency { AgencyId = 2, ShortName = "Monkhouse", LongName = null });
            expected.Add(new Agency { AgencyId = 3, ShortName = "Nutley", LongName = "Bubblebox" });
            expected.Add(new Agency { AgencyId = 4, ShortName = "Perago", LongName = "Pixoboo" });
            expected.Add(new Agency { AgencyId = 5, ShortName = "Quarton", LongName = "Youspan" });
            expected.Add(new Agency { AgencyId = 6, ShortName = "Mangon", LongName = null});
            expected.Add(new Agency { AgencyId = 7, ShortName = "Tebbit", LongName = null });
            expected.Add(new Agency { AgencyId = 8, ShortName = "Vanderson", LongName = "Jabberstorm" });
            expected.Add(new Agency { AgencyId = 9, ShortName = "Gidman", LongName = "Yamia" });
            expected.Add(new Agency { AgencyId = 10, ShortName = "Dondon", LongName = null });
            expected.Add(new Agency { AgencyId = 11, ShortName = "Mabbett", LongName = null });
            expected.Add(new Agency { AgencyId = 12, ShortName = "Hourihan", LongName = "Ailane" });
            expected.Add(new Agency { AgencyId = 13, ShortName = "Gerlts", LongName = "Mybuzz" });
            expected.Add(new Agency { AgencyId = 14, ShortName = "Cory", LongName = null });
            expected.Add(new Agency { AgencyId = 15, ShortName = "Baskerville", LongName = "DabZ" });

            Response<List<Agency>> actual = new Response<List<Agency>>();
            actual = db.GetAll();

            Assert.AreEqual(actual.Data, expected);
        }

        [Test]
        public void Delete_GivenAgencyId_DeleteAgency()
        {
            Response result = db.Delete(1);
            Assert.IsTrue(result.Success);

            Assert.IsFalse(db.Get(1).Success);
        }
        [Test]
        public void Update_GivenAgency_UpdateAgency()
        {
            Response result = db.Update(expectedUpdatedAgency);
            Assert.True(result.Success);

            Agency actual = db.Get(1).Data;
            Assert.AreEqual(expectedUpdatedAgency, actual);
        }

        [Test]
        public void Insert_GivenAgency_InsertAgency()
        {
            Response<Agency> actual = db.Insert(expectedNewAgency);

            Assert.True(actual.Success);
            Assert.AreEqual(expectedNewAgency, actual.Data);
        }
    }
}
