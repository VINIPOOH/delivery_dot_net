using System;
using System.Collections.Generic;
using BLL.dto;
using BLL.exception;
using BLL.impl;
using BLL_TEST.TestConstant;
using DAL;
using DAL.Entity;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BLL_TEST
{
    public class DeliveryServiceTest
    {
        DeliveryService deliveryService;

        private Mock<IWayRepository> wayRepository;

        private Mock<IDeliveryRepository> deliveryRepository;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            wayRepository = new Mock<IWayRepository>();
            deliveryRepository = new Mock<IDeliveryRepository>();
            deliveryService = new DeliveryService(wayRepository.Object, deliveryRepository.Object);
        }


        [Test]
        public void GetDeliveryInfoToGet()
        {
            Delivery delivery = ServicesTestConstant.getDelivery();
            delivery.Bill = ServicesTestConstant.getBill();
            delivery.Bill.User = ServicesTestConstant.getAddreser();
            DeliveryInfoToGetDto deliveryInfoToGetDto = ServicesTestConstant.getDeliveryInfoToGetDto();
            deliveryInfoToGetDto.LocalityGetName = delivery.Way.LocalityGet.NameEn;
            deliveryInfoToGetDto.LocalitySandName = delivery.Way.LocalitySand.NameEn;

            deliveryRepository.Setup(s => s.FindAllByAddressee_IdAndIsPackageReceivedFalseAndBill_IsDeliveryPaidTrue(
                ServicesTestConstant.getUserId())).Returns(new List<Delivery>{delivery});

            List<DeliveryInfoToGetDto> result = deliveryService.GetDeliveryInfoToGet(ServicesTestConstant.getUserId());

            deliveryRepository.Verify(
                place =>
                    place.FindAllByAddressee_IdAndIsPackageReceivedFalseAndBill_IsDeliveryPaidTrue(It.IsAny<string>()),
                Times.Once());

            Assert.AreEqual(deliveryInfoToGetDto, result[0]);
            Assert.AreEqual(ServicesTestConstant.getDeliveres().Count, result.Count);
        }

        [Test]
        public void confirmGettingDeliveryAllCorrect()
        {
            Delivery delivery = ServicesTestConstant.getDelivery();
            deliveryRepository.Setup(s => s.FindByIdAndAddressee_IdAndIsPackageReceivedFalse(
                    ServicesTestConstant.getUserId(), ServicesTestConstant.getDeliveryId()))
                .Returns(delivery);

            bool result = deliveryService.ConfirmGettingDelivery(ServicesTestConstant.getUserId(),
                ServicesTestConstant.getDeliveryId());

            deliveryRepository.Verify(
                place =>
                    place.FindByIdAndAddressee_IdAndIsPackageReceivedFalse(It.IsAny<string>(), It.IsAny<long>()),
                Times.Once());
            deliveryRepository.Verify(
                place =>
                    place.Save(),
                Times.Once());

            Assert.IsTrue(result);
            Assert.IsTrue(delivery.IsPackageReceived);
        }

        [Test]
        public void confirmGettingDeliveryIsNoExistDelivery()
        {
            deliveryRepository.Setup(s => s.FindByIdAndAddressee_IdAndIsPackageReceivedFalse(
                    ServicesTestConstant.getUserId(), ServicesTestConstant.getDeliveryId()))
                .Returns((Delivery) null);
            var actualResult =
                Assert.Throws<AskedDataIsNotExist>(() => deliveryService.ConfirmGettingDelivery(
                    ServicesTestConstant.getUserId(),
                    ServicesTestConstant.getDeliveryId()));
            Assert.AreEqual(typeof(AskedDataIsNotExist), actualResult.GetType());
        }

        [Test]
        public void getDeliveryCostAndTimeDtoAllCorrect()
        {
            DeliveryInfoRequestModel deliveryInfoRequestDto = getDeliveryInfoRequestDto(1);
            PriceAndTimeOnDeliveryModel priceAndTimeOnDeliveryDto = getPriceAndTimeOnDeliveryDto();
            Delivery delivery = ServicesTestConstant.getDelivery();
            Way way = delivery.Way;
            wayRepository.Setup(s => s.FindByLocalitySand_IdAndLocalityGet_Id
                (It.IsAny<long>(), It.IsAny<long>())).Returns(way);

            PriceAndTimeOnDeliveryModel result = deliveryService.GetDeliveryCostAndTimeDto(deliveryInfoRequestDto);
            wayRepository.Verify(
                s => s.FindByLocalitySand_IdAndLocalityGet_Id
                    (It.IsAny<long>(), It.IsAny<long>()), Times.Once());
            Assert.AreEqual(priceAndTimeOnDeliveryDto, result);
        }

        [Test]
        public void getDeliveryCostAndTimeIncorrectWay()
        {
            DeliveryInfoRequestModel deliveryInfoRequestDto = getDeliveryInfoRequestDto(1);
            PriceAndTimeOnDeliveryModel priceAndTimeOnDeliveryDto = getPriceAndTimeOnDeliveryDto();
            wayRepository.Setup(s => s.FindByLocalitySand_IdAndLocalityGet_Id
                (It.IsAny<long>(), It.IsAny<long>())).Returns((Way) null);

            var actualResult =
                Assert.Throws<NoSuchWayException>(() =>
                    deliveryService.GetDeliveryCostAndTimeDto(deliveryInfoRequestDto));
            Assert.AreEqual(typeof(NoSuchWayException), actualResult.GetType());
        }

        [Test]
        public void getDeliveryCostAndTimeIncorrectWeightFactorBiggerOnOne()
        {
            int weightRangeMax = 2;
            int weightRangeReal = 2;
            DeliveryInfoRequestModel deliveryInfoRequestDto = getDeliveryInfoRequestDto(weightRangeReal);
            Delivery delivery = ServicesTestConstant.getDelivery();
            Way way = delivery.Way;
            way.WayToTariffWeightFactors[0].TariffWeightFactor.MaxWeightRange = weightRangeMax;
            wayRepository.Setup(s => s.FindByLocalitySand_IdAndLocalityGet_Id
                (It.IsAny<long>(), It.IsAny<long>())).Returns(way);
            
            var actualResult =
                Assert.Throws<UnsupportableWeightFactorException>(() =>
                    deliveryService.GetDeliveryCostAndTimeDto(deliveryInfoRequestDto));
            Assert.AreEqual(typeof(UnsupportableWeightFactorException), actualResult.GetType());
        }
        
        private DeliveryInfoRequestModel getDeliveryInfoRequestDto(int weightRangeReal)
        {
            return new DeliveryInfoRequestModel(weightRangeReal, 2, 1);
        }

        private PriceAndTimeOnDeliveryModel getPriceAndTimeOnDeliveryDto()
        {
            return new PriceAndTimeOnDeliveryModel(2, 1);
        }
    }
}