namespace BLL.dto
{
    public class PriceAndTimeOnDeliveryModel
    {
        public int CostInCents { get; set; }
        public int TimeOnWayInHours{ get; set; }

        public PriceAndTimeOnDeliveryModel(int costInCents, int timeOnWayInHours)
        {
            CostInCents = costInCents;
            TimeOnWayInHours = timeOnWayInHours;
        }

        protected bool Equals(PriceAndTimeOnDeliveryModel other)
        {
            return CostInCents == other.CostInCents && TimeOnWayInHours == other.TimeOnWayInHours;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            PriceAndTimeOnDeliveryModel priceAndTimeOnDeliveryModel = (PriceAndTimeOnDeliveryModel) obj;
            return CostInCents.Equals(priceAndTimeOnDeliveryModel.CostInCents) &&
                   TimeOnWayInHours.Equals(priceAndTimeOnDeliveryModel.TimeOnWayInHours);
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