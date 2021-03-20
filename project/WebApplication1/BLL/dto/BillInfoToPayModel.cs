namespace BLL.dto
{
    public class BillInfoToPayModel
    {
        public BillInfoToPayModel(long billId, long price, long deliveryId, int weight, string addreeseeEmail)
        {
            BillId = billId;
            Price = price;
            DeliveryId = deliveryId;
            Weight = weight;
            AddreeseeEmail = addreeseeEmail;
        }

        public BillInfoToPayModel()
        {
        }

        public long BillId { get; set; }
        public long Price { get; set; }
        public long DeliveryId { get; set; }
        public int Weight { get; set; }
        public string AddreeseeEmail { get; set; }
        public string LocalitySandName { get; set; }
        public string LocalityGetName { get; set; }

        protected bool Equals(BillInfoToPayModel other)
        {
            return BillId == other.BillId && Price == other.Price && DeliveryId == other.DeliveryId && Weight == other.Weight && AddreeseeEmail == other.AddreeseeEmail && LocalitySandName == other.LocalitySandName && LocalityGetName == other.LocalityGetName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BillInfoToPayModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = BillId.GetHashCode();
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                hashCode = (hashCode * 397) ^ DeliveryId.GetHashCode();
                hashCode = (hashCode * 397) ^ Weight;
                hashCode = (hashCode * 397) ^ (AddreeseeEmail != null ? AddreeseeEmail.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LocalitySandName != null ? LocalitySandName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LocalityGetName != null ? LocalityGetName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}