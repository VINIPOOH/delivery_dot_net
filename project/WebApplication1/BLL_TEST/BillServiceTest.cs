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
        private BillService _billService;
        private Mock<IBillRepository> _billRepository;
        private Mock<IUserRepository> _userRepository;
        private Mock<IDeliveryRepository> _deliveryRepository;
        private Mock<IWayRepository> _wayRepository;
        private int _defaultCostInCents = 2;
        private long _defaultBillId = 0;
        private long _defaultDeliveryId = 0;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            _billRepository = new Mock<IBillRepository>();
            _userRepository=new Mock<IUserRepository>();
            _deliveryRepository=new Mock<IDeliveryRepository>();
            _wayRepository=new Mock<IWayRepository>();

            _billRepository.Setup(a => a.FindByIdAndIsDeliveryPaidFalse(ServicesTestConstant.getBillId()))
                .Returns(ServicesTestConstant.getBill());
            _billRepository.Setup(a => a.FindAllByUserIdAndIsDeliveryPaidFalse(It.IsAny<string>()))
                .Returns(ServicesTestConstant.getBills());

            _userRepository.Setup(a => a.FindByIdAndUserMoneyInCentsGreaterThanEqual
                (ServicesTestConstant.getUserId(),ServicesTestConstant.getBill().CostInCents)
                ).Returns(ServicesTestConstant.getAddreser());

            _userRepository.Setup(a => a.FindByEmail(It.IsAny<string>())
            ).Returns(ServicesTestConstant.getAdversee());
            _userRepository.Setup(a => a.FindByName(It.IsAny<string>())
            ).Returns(ServicesTestConstant.getAdversee());
            
            _wayRepository.Setup(a => a.FindByLocalitySand_IdAndLocalityGet_Id
                (It.IsAny<long>(), It.IsAny<long>())
            ).Returns(ServicesTestConstant.getWay());
            _billService = new BillService
                (_billRepository.Object, _userRepository.Object,_deliveryRepository.Object,_wayRepository.Object);
        }

        [Test]
        public void initializeBillCorrect(){
            Bill bill = ServicesTestConstant.getBill();
            bill.CostInCents=_defaultCostInCents;
            bill.BillId=_defaultBillId;
            bill.IsDeliveryPaid = false;
            Delivery delivery = ServicesTestConstant.getDelivery();
            delivery.DeliveryId  = _defaultDeliveryId;
            _wayRepository.Setup(a => a.FindByLocalitySand_IdAndLocalityGet_Id
                (It.IsAny<long>(), It.IsAny<long>())
            ).Returns(ServicesTestConstant.getWay());

            Bill billResult = _billService.InitializeBill(ServicesTestConstant.getDeliveryOrderCreateDto(),
                ServicesTestConstant.getUserId());

            Assert.AreEqual(bill, billResult);
        }
        
        [Test]
        public void initializeBillCorrectInCorrectAddressee(){
            Bill bill = ServicesTestConstant.getBill();
            bill.CostInCents=_defaultCostInCents;
            bill.BillId=_defaultBillId;
            bill.IsDeliveryPaid = false;
            Delivery delivery = ServicesTestConstant.getDelivery();
            delivery.DeliveryId  = _defaultDeliveryId;
            _userRepository.Setup(a => a.FindByEmail(It.IsAny<string>())
            ).Returns(null as User);

            var actualResult =
                Assert.Throws<NoSuchUserException>(() => _billService.InitializeBill
                    (ServicesTestConstant.getDeliveryOrderCreateDto(), ServicesTestConstant.getUserId()));

            Assert.AreEqual(typeof(NoSuchUserException), actualResult.GetType());
        }

        [Test]
        public void initializeBillIncorrectInWay(){
            Bill bill = ServicesTestConstant.getBill();
            bill.CostInCents=_defaultCostInCents;
            bill.BillId=_defaultBillId;
            bill.IsDeliveryPaid = false;
            Delivery delivery = ServicesTestConstant.getDelivery();
            delivery.DeliveryId  = _defaultDeliveryId;
            
            _wayRepository.Setup(a => a.FindByLocalitySand_IdAndLocalityGet_Id
                (It.IsAny<long>(), It.IsAny<long>())
            ).Returns((Way) null);

            var actualResult =
                Assert.Throws<NoSuchWayException>(()=>_billService.InitializeBill
                    (ServicesTestConstant.getDeliveryOrderCreateDto(), ServicesTestConstant.getUserId()));

            Assert.AreEqual(typeof(NoSuchWayException), actualResult.GetType());
        }

        [Test]
        public void getBillsToPayByUserId()
        {
            BillInfoToPayModel billInfoToPayDto = ServicesTestConstant.getBillInfoToPayDto();
            Bill bill = ServicesTestConstant.getBill();
            billInfoToPayDto.LocalityGetName = bill.Delivery.Way.LocalityGet.NameEn;
            billInfoToPayDto.LocalitySandName = bill.Delivery.Way.LocalitySand.NameEn;

            List<BillInfoToPayModel> result = _billService.GetBillsToPayByUserName(ServicesTestConstant.getUserId());

            _billRepository.Verify(place => place.FindAllByUserIdAndIsDeliveryPaidFalse
                (ServicesTestConstant.getUserId()), Times.Once());
            Assert.AreEqual(ServicesTestConstant.getBills().Count, result.Count);
            Assert.AreEqual(billInfoToPayDto, result[0]);
        }

        [Test]
        public void getBillsToPayByUserIdUserIsNotExist()
        {
            _billRepository.Setup(a => a.FindAllByUserIdAndIsDeliveryPaidFalse(It.IsAny<string>())).Returns(new List<Bill>());

            List<BillInfoToPayModel> billInfoToPayDtos = _billService.GetBillsToPayByUserName(ServicesTestConstant.getUserId());

            _billRepository.Verify(place => place.FindAllByUserIdAndIsDeliveryPaidFalse(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(0, billInfoToPayDtos.Count);
        }

        [Test]
        public void payForDeliveryWhenAllCorrect(){
            Bill bill = ServicesTestConstant.getBill();
            bill.IsDeliveryPaid = false;
            _billRepository.Setup(a => a.FindByIdAndIsDeliveryPaidFalse(ServicesTestConstant.getBillId())).Returns(bill);
            
            bool payResult = _billService.PayForDelivery( ServicesTestConstant.getUserId(),  ServicesTestConstant.getBillId());

            _billRepository.Verify
                (place => place.FindByIdAndIsDeliveryPaidFalse(ServicesTestConstant.getBillId()), Times.Once());
            _userRepository.Verify(place => place.FindByIdAndUserMoneyInCentsGreaterThanEqual
                (ServicesTestConstant.getUserId(), bill.CostInCents), Times.Once());

            Assert.IsTrue(payResult);
            Assert.IsTrue(bill.IsDeliveryPaid);
        }

        [Test]
        public void payForDeliveryNotEnoughMoney(){
            Bill bill = ServicesTestConstant.getBill();
            bill.IsDeliveryPaid=false;
            User adverser = ServicesTestConstant.getAddreser();
            adverser.UserMoneyInCents = 0L;
            _userRepository.Setup(a => a.FindByIdAndUserMoneyInCentsGreaterThanEqual
                (ServicesTestConstant.getUserId(),ServicesTestConstant.getBill().CostInCents)
            ).Returns((User) null);

            var actualResult =
                Assert.Throws<NotEnoughMoneyException>
                    (()=>_billService.PayForDelivery(ServicesTestConstant.getUserId(), ServicesTestConstant.getBillId()));

            Assert.AreEqual(typeof(NotEnoughMoneyException), actualResult.GetType());
        }

        [Test]
        public void payForDeliveryDeliveryAlreadyPaid() {
            _billRepository.Setup(a => a.FindByIdAndIsDeliveryPaidFalse(ServicesTestConstant.getBillId()))
                .Returns((Bill) null);

            var actualResult =
                Assert.Throws<DeliveryAlreadyPaidException>(()=>_billService.PayForDelivery
                    (ServicesTestConstant.getUserId(), ServicesTestConstant.getBillId()));

            Assert.AreEqual(typeof(DeliveryAlreadyPaidException), actualResult.GetType());
        }

        [Test]
        public void getBillHistoryByUserId()
        {
            string userName = "name";
            List<Bill> bills = ServicesTestConstant.getBills();
            _billRepository.Setup(b => b.FindAllByUserNameAndIsDeliveryPaidTrue(userName)).Returns(bills);
            
            List<BillModel> result = _billService.GetBillHistoryByUserName(userName);

            _billRepository.Verify(place => place.FindAllByUserNameAndIsDeliveryPaidTrue(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(ServicesTestConstant.getBillDto(), result[0]);
            Assert.AreEqual(bills.Count, result.Count);
        }
    }
}