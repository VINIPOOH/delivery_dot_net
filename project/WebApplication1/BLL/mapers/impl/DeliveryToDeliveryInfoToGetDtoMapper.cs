using System.Collections.Generic;
using BLL.dto;
using DAL.Entity;

namespace BLL.mapers.impl
{
    public class DeliveryToDeliveryInfoToGetDtoMapper
    {
        public static DeliveryInfoToGetDto map(Delivery delivery)
        {
            return new DeliveryInfoToGetDto(delivery.Bill.User.Email
                , delivery.DeliveryId,
                delivery.Way.LocalitySand.NameEn, delivery.Way.LocalityGet.NameEn);
        }

        public static List<DeliveryInfoToGetDto> mapToList(IEnumerable<Delivery> entities)
        {
            List<DeliveryInfoToGetDto> dtos = new List<DeliveryInfoToGetDto>();
            foreach (var delivery in entities)
            {
                dtos.Add(map(delivery));
            }
            return dtos;
        }
    }
}