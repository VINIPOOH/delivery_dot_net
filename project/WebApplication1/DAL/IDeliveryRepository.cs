using System.Collections;
using System.Collections.Generic;
using ComputerNet.DAL.Interfaces;
using DAL.Entity;

namespace DAL
{
    public interface IDeliveryRepository:IGenericRepository<Delivery>
    {
        IEnumerable<Delivery> findAllByAddressee_IdAndIsPackageReceivedFalseAndBill_IsDeliveryPaidTrue(string billUserId);

        Delivery findByIdAndAddressee_IdAndIsPackageReceivedFalse(string userName, long deliveryId);
    }
}