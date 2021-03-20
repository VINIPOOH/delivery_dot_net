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
    public class BillServiceTest
    {
        private BillService billService;

        private Mock<IBillRepository> billRepository;

        private Mock<IUserRepository> userRepository;

        private Mock<IDeliveryRepository> deliveryRepository;

        private Mock<IWayRepository> wayRepository;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            billRepository = new Mock<IBillRepository>();
            userRepository=new Mock<IUserRepository>();
            deliveryRepository=new Mock<IDeliveryRepository>();
            wayRepository=new Mock<IWayRepository>();

            billRepository.Setup(a => a.FindByIdAndIsDeliveryPaidFalse(ServicesTestConstant.getBillId())).Returns(ServicesTestConstant.getBill());
            billRepository.Setup(a => a.FindAllByUserIdAndIsDeliveryPaidFalse(It.IsAny<string>())).Returns(ServicesTestConstant.getBills());

            userRepository.Setup(a => a.FindByIdAndUserMoneyInCentsGreaterThanEqual(ServicesTestConstant.getUserId(),ServicesTestConstant.getBill().CostInCents)
                ).Returns(ServicesTestConstant.getAddreser());

            userRepository.Setup(a => a.FindByEmail(It.IsAny<string>())
            ).Returns(ServicesTestConstant.getAdversee());
            userRepository.Setup(a => a.FindByName(It.IsAny<string>())
            ).Returns(ServicesTestConstant.getAdversee());
            
            wayRepository.Setup(a => a.FindByLocalitySand_IdAndLocalityGet_Id(It.IsAny<long>(), It.IsAny<long>())
            ).Returns(ServicesTestConstant.getWay());
            billService = new BillService(billRepository.Object, userRepository.Object,deliveryRepository.Object,wayRepository.Object);
        }

        [Test]
        public void initializeBillCorrect(){
            Bill bill = ServicesTestConstant.getBill();
            bill.CostInCents=2;
            bill.BillId=0;
            bill.IsDeliveryPaid = false;
            Delivery delivery = ServicesTestConstant.getDelivery();
            delivery.DeliveryId  = 0;
            wayRepository.Setup(a => a.FindByLocalitySand_IdAndLocalityGet_Id(It.IsAny<long>(), It.IsAny<long>())
            ).Returns(ServicesTestConstant.getWay());

            Bill billResult = billService.InitializeBill(ServicesTestConstant.getDeliveryOrderCreateDto(), ServicesTestConstant.getUserId());

            Assert.AreEqual(bill, billResult);
        }
        [Test]
        public void initializeBillCorrectInCorrectAddressee(){
            Bill bill = ServicesTestConstant.getBill();
            bill.CostInCents=2;
            bill.BillId=0;
            bill.IsDeliveryPaid = false;
            Delivery delivery = ServicesTestConstant.getDelivery();
            delivery.DeliveryId  = 0;
            userRepository.Setup(a => a.FindByEmail(It.IsAny<string>())
            ).Returns(null as User);

            var actualResult =
                Assert.Throws<NoSuchUserException>(() => billService.InitializeBill(ServicesTestConstant.getDeliveryOrderCreateDto(), ServicesTestConstant.getUserId()));

            Assert.AreEqual(typeof(NoSuchUserException), actualResult.GetType());
        }

        [Test]
        public void initializeBillIncorrectInWay(){
            Bill bill = ServicesTestConstant.getBill();
            bill.CostInCents=2;
            bill.BillId=0;
            bill.IsDeliveryPaid = false;
            Delivery delivery = ServicesTestConstant.getDelivery();
            delivery.DeliveryId  = 0;
            
            wayRepository.Setup(a => a.FindByLocalitySand_IdAndLocalityGet_Id(It.IsAny<long>(), It.IsAny<long>())
            ).Returns((Way) null);

            var actualResult =
                Assert.Throws<NoSuchWayException>(()=>billService.InitializeBill(ServicesTestConstant.getDeliveryOrderCreateDto(), ServicesTestConstant.getUserId()));

            Assert.AreEqual(typeof(NoSuchWayException), actualResult.GetType());
        }

        [Test]
        public void getBillsToPayByUserID()
        {
            BillInfoToPayModel billInfoToPayDto = ServicesTestConstant.getBillInfoToPayDto();
            Bill bill = ServicesTestConstant.getBill();
            billInfoToPayDto.LocalityGetName = bill.Delivery.Way.LocalityGet.NameEn;
            billInfoToPayDto.LocalitySandName = bill.Delivery.Way.LocalitySand.NameEn;

            List<BillInfoToPayModel> result = billService.GetBillsToPayByUserName(ServicesTestConstant.getUserId());

            billRepository.Verify(place => place.FindAllByUserIdAndIsDeliveryPaidFalse(ServicesTestConstant.getUserId()), Times.Once());
            Assert.AreEqual(ServicesTestConstant.getBills().Count, result.Count);
            Assert.AreEqual(billInfoToPayDto, result[0]);
        }

        [Test]
        public void getBillsToPayByUserIDUserIsNotExist()
        {
            billRepository.Setup(a => a.FindAllByUserIdAndIsDeliveryPaidFalse(It.IsAny<string>())).Returns(new List<Bill>());

            List<BillInfoToPayModel> billInfoToPayDtos = billService.GetBillsToPayByUserName(ServicesTestConstant.getUserId());

            billRepository.Verify(place => place.FindAllByUserIdAndIsDeliveryPaidFalse(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(0, billInfoToPayDtos.Count);
        }

        [Test]
        public void payForDeliveryWhenAllCorrect(){
            Bill bill = ServicesTestConstant.getBill();
            bill.IsDeliveryPaid = false;
            billRepository.Setup(a => a.FindByIdAndIsDeliveryPaidFalse(ServicesTestConstant.getBillId())).Returns(bill);
            
            bool payResult = billService.PayForDelivery( ServicesTestConstant.getUserId(),  ServicesTestConstant.getBillId());

            billRepository.Verify(place => place.FindByIdAndIsDeliveryPaidFalse(ServicesTestConstant.getBillId()), Times.Once());
            userRepository.Verify(place => place.FindByIdAndUserMoneyInCentsGreaterThanEqual(ServicesTestConstant.getUserId(), bill.CostInCents), Times.Once());

            Assert.IsTrue(payResult);
            Assert.IsTrue(bill.IsDeliveryPaid);
        }

        [Test]
        public void payForDeliveryNotEnoughMoney(){
            Bill bill = ServicesTestConstant.getBill();
            bill.IsDeliveryPaid=false;
            User adverser = ServicesTestConstant.getAddreser();
            adverser.UserMoneyInCents = 0L;
            userRepository.Setup(a => a.FindByIdAndUserMoneyInCentsGreaterThanEqual(ServicesTestConstant.getUserId(),ServicesTestConstant.getBill().CostInCents)
            ).Returns((User) null);

            var actualResult =
                Assert.Throws<NotEnoughMoneyException>(()=>billService.PayForDelivery(ServicesTestConstant.getUserId(), ServicesTestConstant.getBillId()));

            Assert.AreEqual(typeof(NotEnoughMoneyException), actualResult.GetType());
        }

        [Test]
        public void payForDeliveryDeliveryAlreadyPaid() {
            billRepository.Setup(a => a.FindByIdAndIsDeliveryPaidFalse(ServicesTestConstant.getBillId())).Returns((Bill) null);

            var actualResult =
                Assert.Throws<DeliveryAlreadyPaidException>(()=>billService.PayForDelivery(ServicesTestConstant.getUserId(), ServicesTestConstant.getBillId()));

            Assert.AreEqual(typeof(DeliveryAlreadyPaidException), actualResult.GetType());
        }

        [Test]
        public void getBillHistoryByUserId()
        {
            string userName = "name";
            List<Bill> bills = ServicesTestConstant.getBills();
            billRepository.Setup(b => b.FindAllByUserNameAndIsDeliveryPaidTrue(userName)).Returns(bills);
            
            List<BillModel> result = billService.GetBillHistoryByUserName(userName);

            billRepository.Verify(place => place.FindAllByUserNameAndIsDeliveryPaidTrue(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(ServicesTestConstant.getBillDto(), result[0]);
            Assert.AreEqual(bills.Count, result.Count);
        }
    }
}