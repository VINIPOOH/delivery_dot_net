using System.Collections.Generic;
using BLL.dto;
using DAL.Entity;

namespace BLL.Intarfaces
{
    public interface IDeliveryService
    {
        List<DeliveryInfoToGetDto> GetDeliveryInfoToGet(string userName);

        bool ConfirmGettingDelivery(string userName, long deliveryId);

        PriceAndTimeOnDeliveryModel GetDeliveryCostAndTimeDto(DeliveryInfoRequestModel deliveryInfoRequestDto);
    }
}