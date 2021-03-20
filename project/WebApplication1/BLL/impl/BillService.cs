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
            _billRepository = billRepository;
            _userRepository = userRepository;
            _deliveryRepository = deliveryRepository;
            _wayRepository = wayRepository;
        }

        public List<BillInfoToPayModel> GetBillsToPayByUserName(string userName)
        {
            return BillInfoToPayDtoMapper.mapToList(_billRepository.FindAllByUserIdAndIsDeliveryPaidFalse(userName));
        }

        public bool PayForDelivery(string userName, long billId)
        {
            Bill bill = _billRepository.FindByIdAndIsDeliveryPaidFalse(billId);
            if (bill == null)
            {
                throw new DeliveryAlreadyPaidException();
            }
            User user = _userRepository.FindByIdAndUserMoneyInCentsGreaterThanEqual(userName, bill.CostInCents);
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

        public Bill InitializeBill(DeliveryOrderCreateModel deliveryOrderCreateDto, string initiatorName)
        {
            User addressee = _userRepository.FindByEmail(deliveryOrderCreateDto.AddresseeEmail);
            if (addressee == null)
            {
                throw new NoSuchUserException();
            }
            Way way = _wayRepository.FindByLocalitySand_IdAndLocalityGet_Id(deliveryOrderCreateDto.LocalitySandId
                , deliveryOrderCreateDto.LocalityGetId);
            if (way == null)
            {
                throw new NoSuchWayException();
            }
            Delivery newDelivery = getBuildDelivery(deliveryOrderCreateDto, addressee, way);
            _deliveryRepository.Create(newDelivery);
            User user = _userRepository.FindByName(initiatorName);
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

        public List<BillModel> GetBillHistoryByUserName(string userName)
        {
            return BillToBillDtoMapper.mapToList(
                _billRepository.FindAllByUserNameAndIsDeliveryPaidTrue(userName));
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