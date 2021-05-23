using System.Collections.Generic;
using BLL.dto;
using BLL.impl;
using DAL.Entity;
using DAL.impl;
using NUnit.Framework;

namespace integration_test
{
    public class LocalitiesServiceTest
    {
        private LocalityService _localityService;
        private MockDbContext _context;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            _context = new MockDbContext();
            _localityService = new LocalityService(new LocalityRepository(_context));
        }
        
        [Test]
        public void GetLocalities()
        {
            Way way = EntitySetuper.SetupWayWithTarif(_context);
            LocalityModel expectedGet = new LocalityModel(way.LocalityGetLocalityId,way.LocalityGet.NameEn);
            LocalityModel expectedSend = new LocalityModel(way.LocalitySandLocalityId,way.LocalitySand.NameEn);

            List<LocalityModel> result = _localityService.GetLocalities();

            Assert.AreEqual(expectedSend, result[0]);
            Assert.AreEqual(expectedGet, result[1]);
            Assert.AreEqual(2, result.Count);
        }
    }
}