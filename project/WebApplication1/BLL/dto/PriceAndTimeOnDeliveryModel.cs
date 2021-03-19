namespace BLL.dto
{
    public class PriceAndTimeOnDeliveryModel
    {
        public int CostInCents { get; set; }
        public int TimeOnWayInHours{ get; set; }

        public PriceAndTimeOnDeliveryModel(int costInCents, int timeOnWayInHours)
        {
            this.CostInCents = costInCents;
            this.TimeOnWayInHours = timeOnWayInHours;
        }

        protected bool Equals(PriceAndTimeOnDeliveryModel other)
        {
            return CostInCents == other.CostInCents && TimeOnWayInHours == other.TimeOnWayInHours;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PriceAndTimeOnDeliveryModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (CostInCents * 397) ^ TimeOnWayInHours;
            }
        }
    }
}