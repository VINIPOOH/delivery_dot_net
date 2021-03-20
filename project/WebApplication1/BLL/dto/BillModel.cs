using System;

namespace BLL.dto
{
    public class BillModel
    {
        public BillModel()
        {
        }

        public BillModel(long deliveryId, long id, bool isDeliveryPaid, long costInCents)
        {
            DeliveryId = deliveryId;
            Id = id;
            IsDeliveryPaid = isDeliveryPaid;
            CostInCents = costInCents;
        }

        public long DeliveryId{ get; set; }
        public long Id{ get; set; }
        public bool IsDeliveryPaid{ get; set; }
        public long CostInCents{ get; set; }
        public DateTime DateOfPay{ get; set; }

        protected bool Equals(BillModel other)
        {
            return DeliveryId == other.DeliveryId && Id == other.Id && IsDeliveryPaid == other.IsDeliveryPaid && CostInCents == other.CostInCents && DateOfPay.Equals(other.DateOfPay);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BillModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = DeliveryId.GetHashCode();
                hashCode = (hashCode * 397) ^ Id.GetHashCode();
                hashCode = (hashCode * 397) ^ IsDeliveryPaid.GetHashCode();
                hashCode = (hashCode * 397) ^ CostInCents.GetHashCode();
                hashCode = (hashCode * 397) ^ DateOfPay.GetHashCode();
                return hashCode;
            }
        }
    }
}