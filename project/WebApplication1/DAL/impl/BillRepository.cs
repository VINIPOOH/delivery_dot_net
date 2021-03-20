using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ComputerNet.DAL.Repositories;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dal;

namespace DAL.impl
{
    public class BillRepository : GenericRepository<Bill>, IBillRepository
    {
        public BillRepository(MyDbContext context) : base(context)
        {
        }

        public IEnumerable<Bill> FindAllByUserIdAndIsDeliveryPaidFalse(string userId)
        {
            return Context.Bills.Include(u => u.User).Include(d => d.Delivery).ThenInclude(w => w.Way)
                .ThenInclude(l => l.LocalitySand).Include(d => d.Delivery).ThenInclude(w => w.Way)
                .ThenInclude(l => l.LocalityGet)
                .Where(bill => bill.User.UserName.Equals(userId) && !bill.IsDeliveryPaid);
        }

        public IEnumerable<Bill> FindAllByUserNameAndIsDeliveryPaidTrue(string userName)
        {
            return base.Get(bill => bill.User.UserName.Equals(userName) && bill.IsDeliveryPaid);
        }

        public Bill FindByIdAndIsDeliveryPaidFalse(long billId)
        {
            return base.Get(bill => bill.BillId.Equals(billId) && !bill.IsDeliveryPaid).FirstOrDefault();
        }
    }
}