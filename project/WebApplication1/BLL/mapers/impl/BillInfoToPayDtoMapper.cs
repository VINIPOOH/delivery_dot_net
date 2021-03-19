using System.Collections.Generic;
using BLL.dto;
using DAL.Entity;

namespace BLL.mapers.impl
{
    public class BillInfoToPayDtoMapper
    {
        public static BillInfoToPayModel map(Bill bill)
        {
            BillInfoToPayModel billInfoToPayModel = new BillInfoToPayModel();
            billInfoToPayModel.Weight = bill.Delivery.Weight;
            billInfoToPayModel.Price = bill.CostInCents;
            billInfoToPayModel.DeliveryId = bill.Delivery.DeliveryId;
            billInfoToPayModel.BillId = bill.BillId;
            billInfoToPayModel.AddreeseeEmail = bill.Delivery.Addressee.Email;
            billInfoToPayModel.LocalitySandName = bill.Delivery.Way.LocalitySand.NameEn;
            billInfoToPayModel.LocalityGetName = bill.Delivery.Way.LocalityGet.NameEn;
            return billInfoToPayModel;
        }

        public static List<BillInfoToPayModel> mapToList(IEnumerable<Bill> entities)
        {
            List<BillInfoToPayModel> dtos = new List<BillInfoToPayModel>();
            foreach (var delivery in entities)
            {
                dtos.Add(map(delivery));
            }
            return dtos;
        }
    }
}