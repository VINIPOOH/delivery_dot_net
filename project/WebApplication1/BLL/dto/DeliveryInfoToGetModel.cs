using System;

namespace BLL.dto
{
    public class DeliveryInfoToGetDto
    {
        public DeliveryInfoToGetDto(string addresserEmail, long deliveryId, string localitySandName, string localityGetName)
        {
            AddresserEmail = addresserEmail;
            DeliveryId = deliveryId;
            LocalitySandName = localitySandName;
            LocalityGetName = localityGetName;
        }

        public string AddresserEmail{ get; set; }
        public long DeliveryId{ get; set; }
        public string LocalitySandName{ get; set; }

        protected bool Equals(DeliveryInfoToGetDto other)
        {
            return AddresserEmail == other.AddresserEmail && DeliveryId == other.DeliveryId && LocalitySandName == other.LocalitySandName && LocalityGetName == other.LocalityGetName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DeliveryInfoToGetDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (AddresserEmail != null ? AddresserEmail.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ DeliveryId.GetHashCode();
                hashCode = (hashCode * 397) ^ (LocalitySandName != null ? LocalitySandName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LocalityGetName != null ? LocalityGetName.GetHashCode() : 0);
                return hashCode;
            }
        }

        public string LocalityGetName{ get; set; }
    }
}