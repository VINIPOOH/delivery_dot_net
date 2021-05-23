using System.Collections.Generic;
using BLL.dto;
using BLL.exception;
using BLL.impl;
using BLL_TEST.TestConstant;
using DAL.Entity;
using DAL.impl;
using NUnit.Framework;

namespace integration_test
{
    public class DeliveryServiceTests
    {
        private DeliveryService _deliveryService;
        private MockDbContext _context;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            _context = new MockDbContext();
            _deliveryService = new DeliveryService(new WayRepository(_context), new DeliveryRepository(_context));
        }


        [Test]
        public void GetDeliveryInfoToGet()
        {
            Delivery delivery = EntitySetuper.SetupDeliveryAndBill(_context, false, true);
            DeliveryInfoToGetDto deliveryInfoToGetDto = ServicesTestConstant.getDeliveryInfoToGetDto();
            deliveryInfoToGetDto.LocalityGetName = delivery.Way.LocalityGet.NameEn;
            deliveryInfoToGetDto.LocalitySandName = delivery.Way.LocalitySand.NameEn;

            List<DeliveryInfoToGetDto> result = _deliveryService.GetDeliveryInfoToGet(delivery.Addressee.UserName);

            Assert.AreEqual(deliveryInfoToGetDto, result[0]);
            Assert.AreEqual(ServicesTestConstant.getDeliveres().Count, result.Count);
        }

        [Test]
        public void confirmGettingDeliveryAllCorrect()
        {
            Delivery delivery = EntitySetuper.SetupDeliveryAndBill(_context, false, true);

            bool result = _deliveryService.ConfirmGettingDelivery(delivery.Addressee.UserName, delivery.DeliveryId);

            Assert.IsTrue(result);
            Assert.IsTrue(delivery.IsPackageReceived);
        }

        [Test]
        public void confirmGettingDeliveryIsNoExistDelivery()
        {
            User adresee = EntitySetuper.SetupAdresee(_context);
            long randomNotExsistDeliveryId = 100L;

            var actualResult =
                Assert.Throws<AskedDataIsNotExist>(() => _deliveryService.ConfirmGettingDelivery(
                    adresee.UserName,
                    randomNotExsistDeliveryId));
            Assert.AreEqual(typeof(AskedDataIsNotExist), actualResult.GetType());
        }

        [Test]
        public void getDeliveryCostAndTimeDtoAllCorrect()
        {
            Way way = EntitySetuper.SetupWayWithTarif(_context);
            DeliveryInfoRequestModel deliveryInfoRequestDto =
                new DeliveryInfoRequestModel(10, way.LocalitySandLocalityId, way.LocalityGetLocalityId);
            PriceAndTimeOnDeliveryModel priceAndTimeOnDeliveryModel = new PriceAndTimeOnDeliveryModel(200, 2);

            PriceAndTimeOnDeliveryModel result = _deliveryService.GetDeliveryCostAndTimeDto(deliveryInfoRequestDto);

            Assert.AreEqual(priceAndTimeOnDeliveryModel, result);
        }

        [Test]
        public void getDeliveryCostAndTimeIncorrectWay()
        {
            Way way = EntitySetuper.SetupWayWithTarif(_context);
            int notExistLocalitySend = 300;
            DeliveryInfoRequestModel deliveryInfoRequestDto =
                new DeliveryInfoRequestModel(10, notExistLocalitySend, way.LocalityGetLocalityId);

            var actualResult =
                Assert.Throws<NoSuchWayException>(() =>
                    _deliveryService.GetDeliveryCostAndTimeDto(deliveryInfoRequestDto));

            Assert.AreEqual(typeof(NoSuchWayException), actualResult.GetType());
        }


        [Test]
        public void getDeliveryCostAndTimeIncorrectWeightFactorBiggerOnOne()
        {
            Way way = EntitySetuper.SetupWayWithTarif(_context);
            int weightBigerOnOneOfMaximumTarifWeightFactor =
                EntitySetuper.MAX_WRIGHT_ON_SETUPED_TARIF_WEIGHT_FACTOR + 1;
            DeliveryInfoRequestModel deliveryInfoRequestDto =
                new DeliveryInfoRequestModel(weightBigerOnOneOfMaximumTarifWeightFactor, way.LocalitySandLocalityId,
                    way.LocalityGetLocalityId);

            var actualResult =
                Assert.Throws<UnsupportableWeightFactorException>(() =>
                    _deliveryService.GetDeliveryCostAndTimeDto(deliveryInfoRequestDto));

            Assert.AreEqual(typeof(UnsupportableWeightFactorException), actualResult.GetType());
        }
    }
}