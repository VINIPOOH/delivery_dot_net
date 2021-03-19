using System.Collections.Generic;
using BLL.dto;
using DAL.Entity;

namespace BLL.Intarfaces
{
    public interface IDeliveryService
         {
        List<DeliveryInfoToGetDto> getDeliveryInfoToGet(string userName);

        bool confirmGettingDelivery(string userName, long deliveryId);

        PriceAndTimeOnDeliveryModel getDeliveryCostAndTimeDto(DeliveryInfoRequestModel deliveryInfoRequestDto);

    }
}