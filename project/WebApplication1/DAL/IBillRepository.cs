using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ComputerNet.DAL.Interfaces;
using DAL.Entity;
using Utils;

namespace DAL
{
    public interface IBillRepository:IGenericRepository<Bill>
    {
        IEnumerable<Bill> findAllByUserIdAndIsDeliveryPaidFalse(string userId);

        IEnumerable<Bill> findAllByUserNameAndIsDeliveryPaidTrue(string userId);

        Bill findByIdAndIsDeliveryPaidFalse(long billId);
    }
}