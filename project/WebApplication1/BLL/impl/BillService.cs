using System;
using System.Collections.Generic;
using BLL.dto;
using BLL.exception;
using BLL.Intarfaces;
using BLL.mapers.impl;
using DAL;
using DAL.Entity;

namespace BLL.impl
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;
        private readonly IUserRepository _userRepository;
        private IDeliveryRepository _deliveryRepository;
        private IWayRepository _wayRepository;

        public BillService(IBillRepository billRepository, IUserRepository userRepository,
            IDeliveryRepository deliveryRepository, IWayRepository wayRepository)
        {
            this._billRepository = billRepository;
            this._userRepository = userRepository;
            this._deliveryRepository = deliveryRepository;
            this._wayRepository = wayRepository;
        }

        public List<BillInfoToPayModel> getBillsToPayByUserName(string userName)
        {
            return BillInfoToPayDtoMapper.mapToList(_billRepository.findAllByUserIdAndIsDeliveryPaidFalse(userName));
        }

        public bool payForDelivery(string userName, long billId)
        {
            Bill bill = _billRepository.findByIdAndIsDeliveryPaidFalse(billId);
            if (bill == null)
            {
                throw new DeliveryAlreadyPaidException();
            }
            User user = _userRepository.findByIdAndUserMoneyInCentsGreaterThanEqual(userName, bill.CostInCents);
            if (user == null)
            {
                throw new NotEnoughMoneyException();
            }

            user.UserMoneyInCents = (user.UserMoneyInCents - bill.CostInCents);
            bill.IsDeliveryPaid = true;
            bill.DateOfPay = DateTime.Now;
            _userRepository.Save();
            _billRepository.Save();
            return true;
        }

        public Bill initializeBill(DeliveryOrderCreateModel deliveryOrderCreateDto, string initiatorName)
        {
            User addressee = _userRepository.findByEmail(deliveryOrderCreateDto.AddresseeEmail);
            if (addressee == null)
            {
                throw new NoSuchUserException();
            }

            Way way = _wayRepository.findByLocalitySand_IdAndLocalityGet_Id(deliveryOrderCreateDto.LocalitySandId
                , deliveryOrderCreateDto.LocalityGetId);
            if (way == null)
            {
                throw new NoSuchWayException();
            }

            Delivery newDelivery = getBuildDelivery(deliveryOrderCreateDto, addressee, way);
            _deliveryRepository.Create(newDelivery);
            User user = _userRepository.findByName(initiatorName);
            if (user == null)
            {
                throw new NoSuchUserException();
            }

            Bill buildBill = getBuildBill(newDelivery
                , calculateDeliveryCost(deliveryOrderCreateDto.DeliveryWeight, way)
                , user);
            _billRepository.Create(
                buildBill);
            _billRepository.Save();
            return buildBill;
        }

        public List<BillModel> getBillHistoryByUserName(string userName)
        {
            return BillToBillDtoMapper.mapToList(
                _billRepository.findAllByUserNameAndIsDeliveryPaidTrue(userName));
        }

        private Bill getBuildBill(Delivery newDelivery, long cost, User sender)
        {
            Bill bill = new Bill();
            bill.Delivery = newDelivery;
            bill.DeliveryId = newDelivery.DeliveryId;
            bill.User = sender;
            bill.UserId = sender.Id;
            bill.CostInCents = cost;
            return bill;
        }

        private Delivery getBuildDelivery(DeliveryOrderCreateModel deliveryOrderCreateDto, User addressee, Way way)
        {
            Delivery delivery = new Delivery();
            delivery.Addressee = addressee;
            delivery.AddresseeUserId = addressee.Id;
            delivery.Way = way;
            delivery.WayId = way.WayId;
            delivery.Weight = deliveryOrderCreateDto.DeliveryWeight;
            return delivery;
        }

        private int calculateDeliveryCost(int deliveryWeight, Way way)
        {
            foreach (var factor in way.WayToTariffWeightFactors)
            {
                if (factor.TariffWeightFactor.MaxWeightRange > deliveryWeight &&
                    factor.TariffWeightFactor.MinWeightRange <= deliveryWeight)
                {
                    return (factor.TariffWeightFactor.OverPayOnKilometer + way.PriceForKilometerInCents) * way.DistanceInKilometres;
                }
            }

            throw new UnsupportableWeightFactorException();
        }
    }
}