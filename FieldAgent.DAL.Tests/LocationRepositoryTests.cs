using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;

namespace FieldAgent.DAL.Tests
{
    public class LocationRepositoryTests
    {
        LocationRepository db;
        DbFactory dbf;

        Location expectedLocation = new Location
        {
            LocationId = 1,
            LocationName = "Lydall, Inc.",
            Street1 = "19453 Grasskamp Court",
            Street2 = null,
            City = "Guaíba",
            PostalCode = "6897",
            CountryCode = "1",

            AgencyId = 1
        };
        Location expectedUpdatedLocation = new Location
        {
            LocationId = 1,
            LocationName = "Updated Lydall, Inc.",
            Street1 = "Updated 19453 Grasskamp Court",
            Street2 = "UpdatedRandomStreet2",
            City = "UpdatedAllen",
            PostalCode = "Updated6897",
            CountryCode = "Up1",

            AgencyId = 2
        };
        Location expectedNewLocation = new Location
        {
            LocationName = "New Lydall, Inc.",
            Street1 = "New 19453 Grasskamp Court",
            Street2 = "randomStreet2",
            City = "Allen",
            PostalCode = "new6897",
            CountryCode = "new1",

            AgencyId = 1
        };

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DbFactory(cp.Config, FactoryMode.TEST);
            db = new LocationRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void Get_GivenLocationId_ReturnLocation()
        {
            Response<Location> actual = db.Get(1);
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Data, expectedLocation);
        }

        [Test]
        public void Insert_GivenLocation_InsertLocation()
        {
            Response<Location> actual = db.Insert(expectedNewLocation);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Data, expectedNewLocation);
        }

        [Test]
        public void Delete_GivenLocationId_DeleteLocation()
        {
            Response actual = db.Delete(1);

            Assert.IsTrue(actual.Success);
            Assert.False(db.Get(1).Success);
        }

        [Test]
        public void Update_GivenLocation_UpdateLocation()
        {
            Response result = db.Update(expectedUpdatedLocation);

            Assert.IsTrue(result.Success);

            Location actual = db.Get(1).Data;
            Assert.AreEqual(actual, expectedUpdatedLocation);
        }

        [Test]
        public void GetByAgency_GivenAgencyId_ReturnLocations()
        {
            List<Location> expected = new List<Location>();
            expected.Add(expectedLocation);

            Response<List<Location>> actual = db.GetByAgency(1);

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(actual.Data, expected);
        }
    }
}
