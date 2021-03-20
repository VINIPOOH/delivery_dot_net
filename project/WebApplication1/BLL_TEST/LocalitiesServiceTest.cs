using System.Collections.Generic;
using BLL.dto;
using BLL.impl;
using BLL_TEST.TestConstant;
using DAL;
using DAL.Entity;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BLL_TEST
{
    public class LocalitiesServiceTest
    {
        LocalityService localityService;
        private Mock<ILocalityRepository> localityRepository;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            localityRepository = new Mock<ILocalityRepository>();
            localityService = new LocalityService(localityRepository.Object);
        }
        
        [Test]
        public void GetLocalities() {
            Locality locality = ServicesTestConstant.getLocalityGet();
            List<Locality> localities = new List<Locality>{locality};
            LocalityModel localityDto = new LocalityModel(locality.LocalityId,locality.NameEn);
            localityRepository.Setup(s => s.Get(null,null)).Returns(localities);

            List<LocalityModel> result = localityService.GetLocalities();

            localityRepository.Verify(
                place =>
                    place.Get(null,null),
                Times.Once());
            Assert.AreEqual(localityDto, result[0]);
            Assert.AreEqual(localities.Count, result.Count);
        }
    }
}