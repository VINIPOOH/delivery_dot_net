using System.Collections.Generic;
using BLL.dto;
using DAL.Entity;

namespace BLL.mapers.impl
{
    public class BillToBillDtoMapper
    {
        public static BillModel map(Bill bill)
        {
            BillModel billModel = new BillModel();
            billModel.Id = bill.BillId;
            billModel.DeliveryId = bill.DeliveryId;
            billModel.IsDeliveryPaid = bill.IsDeliveryPaid;
            billModel.CostInCents = bill.CostInCents;
            billModel.DateOfPay = bill.DateOfPay;
            return billModel;
        }

        public static List<BillModel> mapToList(IEnumerable<Bill> entities)
        {
            List<BillModel> dtos = new List<BillModel>();
            foreach (var delivery in entities)
            {
                dtos.Add(map(delivery));
            }
            return dtos;
        }
    }
}