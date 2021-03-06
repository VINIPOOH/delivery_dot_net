using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class Bill
    {
        public long DeliveryId { get; set; }
       public Delivery Delivery { get; set; }
        public long BillId{ get; set; }
        public string
            UserId { get; set; }
        public User User { get; set; }
        public bool IsDeliveryPaid{ get; set; }
        public long CostInCents{ get; set; }
        public DateTime DateOfPay{ get; set; }

        public Bill()
        {
        }
        

        public Bill(long deliveryId, Delivery delivery, long billId, bool isDeliveryPaid, long costInCents,string userId, User user)
        {
            DeliveryId = deliveryId;
            Delivery = delivery;
            BillId = billId;
            IsDeliveryPaid = isDeliveryPaid;
            CostInCents = costInCents;
            UserId = userId;
            User = user;
        }

        public override string ToString()
        {
            return "Bill{}";
        }
        
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            Bill bill = (Bill) obj;
            return BillId == bill.BillId &&
                   IsDeliveryPaid == bill.IsDeliveryPaid &&
                   CostInCents == bill.CostInCents &&
                   UserId.Equals(bill.UserId);
        }
        public override int GetHashCode() {
            return HashCode.Combine(BillId, UserId, IsDeliveryPaid, CostInCents);
        }
        
    }
}