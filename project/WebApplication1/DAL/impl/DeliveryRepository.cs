using System.Collections.Generic;
using System.Linq;
using ComputerNet.DAL.Repositories;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dal;

namespace DAL.impl
{
    public class DeliveryRepository : GenericRepository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(MyDbContext context) : base(context)
        {
        }

        public IEnumerable<Delivery> FindAllByAddressee_IdAndIsPackageReceivedFalseAndBill_IsDeliveryPaidTrue(
            string billUserId)
        {
            return Context.Deliveries.Include(u => u.Addressee).Include(b => b.Bill).ThenInclude(u => u.User)
                .Include(l => l.Way).ThenInclude(g => g.LocalityGet).Include(l => l.Way)
                .ThenInclude(g => g.LocalitySand).Where(delivery =>
                    delivery.Addressee.UserName.Equals(billUserId) && !delivery.IsPackageReceived &&
                    delivery.Bill.IsDeliveryPaid);
        }

        public Delivery FindByIdAndAddressee_IdAndIsPackageReceivedFalse(string userName, long deliveryId)
        {
            return base.Get(delivery =>
                delivery.DeliveryId.Equals(deliveryId) && delivery.Addressee.UserName.Equals(userName) &&
                !delivery.IsPackageReceived).FirstOrDefault();
        }
    }
}