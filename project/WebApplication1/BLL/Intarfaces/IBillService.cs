using System.Collections.Generic;
using BLL.dto;
using DAL.Entity;

namespace BLL.Intarfaces
{
    public interface IBillService
    {
        List<BillInfoToPayModel> GetBillsToPayByUserName(string userName);

        bool PayForDelivery(string userName, long billId);

        Bill InitializeBill(DeliveryOrderCreateModel deliveryOrderCreateDto, string initiatorName);

        List<BillModel> GetBillHistoryByUserName(string userId);

    }
}