using System.Collections.Generic;
using BLL.dto;
using DAL.Entity;

namespace BLL.Intarfaces
{
    public interface IBillService
    {
        List<BillInfoToPayModel> getBillsToPayByUserName(string userName);

        bool payForDelivery(string userName, long billId);

        Bill initializeBill(DeliveryOrderCreateModel deliveryOrderCreateDto, string initiatorName);

        List<BillModel> getBillHistoryByUserName(string userId);

    }
}