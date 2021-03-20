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
        IEnumerable<Bill> FindAllByUserIdAndIsDeliveryPaidFalse(string userId);

        IEnumerable<Bill> FindAllByUserNameAndIsDeliveryPaidTrue(string userId);    

        Bill FindByIdAndIsDeliveryPaidFalse(long billId);
    }
}