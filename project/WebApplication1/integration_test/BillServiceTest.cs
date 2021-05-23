using System;
using System.Collections.Generic;
using BLL.dto;
using BLL.exception;
using BLL.impl;
using BLL_TEST.TestConstant;
using DAL.Entity;
using DAL.impl;
using Moq;
using NUnit.Framework;

namespace integration_test
{
    public class BillServiceTest
    {
        private BillService _billService;
        private MockDbContext _context;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            _context = new MockDbContext();
            _billService = new BillService(new BillRepository(_context), new UserRepository(_context),
                new DeliveryRepository(_context), new WayRepository(_context));
        }

        [Test]
        public void initializeBillCorrect()
        {
            Way setupWayWithTarif = EntitySetuper.SetupWayWithTarif(_context);
            User setupAdresee = EntitySetuper.SetupAdresee(_context);
            User setupAdreser = EntitySetuper.SetupAdreser(_context);
            DeliveryOrderCreateModel deliveryOrderCreateModel = new DeliveryOrderCreateModel(10,
                setupWayWithTarif.LocalitySandLocalityId, setupWayWithTarif.LocalityGetLocalityId, setupAdresee.Email);
            int expectedCost = 200;


            Bill billResult = _billService.InitializeBill(deliveryOrderCreateModel,
                setupAdreser.UserName);

            Assert.AreEqual(setupAdreser.Id, billResult.User.Id);
            Assert.False(billResult.IsDeliveryPaid);
            Assert.False(billResult.Delivery.IsPackageReceived);
            Assert.AreEqual(expectedCost, billResult.CostInCents);
        }

        [Test]
        public void initializeBillCorrectInCorrectAddressee()
        {
            Way setupWayWithTarif = EntitySetuper.SetupWayWithTarif(_context);
            String incorrectAdreseeEmail = "incorrectAdreseeEmail";
            EntitySetuper.SetupAdresee(_context);
            User setupAdreser = EntitySetuper.SetupAdreser(_context);
            DeliveryOrderCreateModel deliveryOrderCreateModel = new DeliveryOrderCreateModel(10,
                setupWayWithTarif.LocalitySandLocalityId, setupWayWithTarif.LocalityGetLocalityId,
                incorrectAdreseeEmail);

            var actualResult =
                Assert.Throws<NoSuchUserException>(() => _billService.InitializeBill(deliveryOrderCreateModel,
                    setupAdreser.UserName)
                );

            Assert.AreEqual(typeof(NoSuchUserException), actualResult.GetType());
        }


        [Test]
        public void initializeBillIncorrectInWay()
        {
            EntitySetuper.SetupWayWithTarif(_context);

            User setupAdresee = EntitySetuper.SetupAdresee(_context);
            User setupAdreser = EntitySetuper.SetupAdreser(_context);
            int incorrectLocalitySandId = 1000;
            int incorrectLocalityGetId = 100;
            DeliveryOrderCreateModel deliveryOrderCreateModel = new DeliveryOrderCreateModel(10,
                incorrectLocalitySandId, incorrectLocalityGetId, setupAdresee.Email);

            var actualResult =
                Assert.Throws<NoSuchWayException>(() => _billService.InitializeBill(deliveryOrderCreateModel,
                    setupAdreser.UserName));

            Assert.AreEqual(typeof(NoSuchWayException), actualResult.GetType());
        }


        [Test]
        public void getBillsToPayByUserId()
        {
            Delivery setupDeliveryAndBill = EntitySetuper.SetupDeliveryAndBill(_context, false, false);
            BillInfoToPayModel expectedBillInfoToPayModel = new BillInfoToPayModel(setupDeliveryAndBill.Bill.BillId,
                setupDeliveryAndBill.Bill.CostInCents, setupDeliveryAndBill.DeliveryId, setupDeliveryAndBill.Weight,
                setupDeliveryAndBill.Addressee.Email);
            expectedBillInfoToPayModel.LocalityGetName = setupDeliveryAndBill.Way.LocalityGet.NameEn;
            expectedBillInfoToPayModel.LocalitySandName = setupDeliveryAndBill.Way.LocalitySand.NameEn;

            List<BillInfoToPayModel> result =
                _billService.GetBillsToPayByUserName(setupDeliveryAndBill.Bill.User.UserName);

            Assert.AreEqual(ServicesTestConstant.getBills().Count, result.Count);
            Assert.AreEqual(expectedBillInfoToPayModel, result[0]);
        }


        [Test]
        public void getBillsToPayByUserIdUserIsNotExist()
        {
            string notexcistusername = "NotExcistUserName";
            List<BillInfoToPayModel> billInfoToPayDtos = _billService.GetBillsToPayByUserName(notexcistusername);

            Assert.AreEqual(0, billInfoToPayDtos.Count);
        }

        [Test]
        public void payForDeliveryWhenAllCorrect()
        {
            Delivery setupDeliveryAndBill = EntitySetuper.SetupDeliveryAndBill(_context, false, false);
            
            bool payResult =
                _billService.PayForDelivery(setupDeliveryAndBill.Bill.User.Email, setupDeliveryAndBill.Bill.BillId);

            Assert.IsTrue(payResult);
            Assert.IsTrue(setupDeliveryAndBill.Bill.IsDeliveryPaid);
        }


        [Test]
        public void payForDeliveryNotEnoughMoney(){
            Delivery setupDeliveryAndBill = EntitySetuper.SetupDeliveryAndBill(_context, false, false);
            setupDeliveryAndBill.Bill.User.UserMoneyInCents = 0;
            _context.SaveChanges();

            var actualResult =
                Assert.Throws<NotEnoughMoneyException>
                    (()=>_billService.PayForDelivery(ServicesTestConstant.getUserId(), ServicesTestConstant.getBillId()));

            Assert.AreEqual(typeof(NotEnoughMoneyException), actualResult.GetType());
        }

        [Test]
        public void payForDeliveryDeliveryAlreadyPaid() {
            Delivery setupDeliveryAndBill = EntitySetuper.SetupDeliveryAndBill(_context, false, true);

            var actualResult =
                Assert.Throws<DeliveryAlreadyPaidException>(()=>_billService.PayForDelivery
                    (ServicesTestConstant.getUserId(), ServicesTestConstant.getBillId()));

            Assert.AreEqual(typeof(DeliveryAlreadyPaidException), actualResult.GetType());
        }

        [Test]
        public void getBillHistoryByUserId()
        {
            Delivery setupDeliveryAndBill = EntitySetuper.SetupDeliveryAndBill(_context, false, true);
            
            List<BillModel> result = _billService.GetBillHistoryByUserName(setupDeliveryAndBill.Bill.User.UserName);

            Assert.AreEqual(setupDeliveryAndBill.DeliveryId, result[0].DeliveryId);
            Assert.AreEqual(setupDeliveryAndBill.Bill.BillId, result[0].Id);
            Assert.True( result[0].IsDeliveryPaid);
            Assert.AreEqual(EntitySetuper.BILL_COST, result[0].CostInCents);
            Assert.AreEqual(1, result.Count);
        }
    }
}