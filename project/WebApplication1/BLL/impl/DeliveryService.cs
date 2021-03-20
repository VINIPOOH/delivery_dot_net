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
    public class DeliveryService : IDeliveryService
    {
        private readonly IWayRepository _wayRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryService(IWayRepository wayRepository, IDeliveryRepository deliveryRepository)
        {
            _wayRepository = wayRepository;
            _deliveryRepository = deliveryRepository;
        }


        public List<DeliveryInfoToGetDto> GetDeliveryInfoToGet(string userName)
        {
            return DeliveryToDeliveryInfoToGetDtoMapper.mapToList(_deliveryRepository
                .FindAllByAddressee_IdAndIsPackageReceivedFalseAndBill_IsDeliveryPaidTrue(userName));
        }

        public bool ConfirmGettingDelivery(string userName, long deliveryId)
        {
            Delivery delivery =
                _deliveryRepository.FindByIdAndAddressee_IdAndIsPackageReceivedFalse(userName, deliveryId);
            if (delivery == null)
            {
                throw new AskedDataIsNotExist();
            }

            delivery.IsPackageReceived = true;
            _deliveryRepository.Save();
            return true;
        }

        public PriceAndTimeOnDeliveryModel GetDeliveryCostAndTimeDto(DeliveryInfoRequestModel deliveryInfoRequestDto)
        {
            Way way = getWay(deliveryInfoRequestDto.LocalitySandId, deliveryInfoRequestDto.LocalityGetId);
            return new PriceAndTimeOnDeliveryModel(calculateDeliveryCost(deliveryInfoRequestDto.DeliveryWeight, way),
                way.TimeOnWayInDays);
        }

        private Way getWay(long localitySandId, long localityGetId)
        {
            Way way = _wayRepository.FindByLocalitySand_IdAndLocalityGet_Id(localitySandId
                , localityGetId);
            if (way == null)
            {
                throw new NoSuchWayException();
            }

            return way;
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