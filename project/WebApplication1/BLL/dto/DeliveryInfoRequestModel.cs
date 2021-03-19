using System.ComponentModel.DataAnnotations;

namespace BLL.dto
{
    public class DeliveryInfoRequestModel
    {
        public DeliveryInfoRequestModel()
        {
        }

        public DeliveryInfoRequestModel(int deliveryWeight, long localitySandId, long localityGetId)
        {
            this.DeliveryWeight = deliveryWeight;
            LocalitySandId = localitySandId;
            LocalityGetId = localityGetId;
        }

        [Range(1, int.MaxValue, ErrorMessage = "Must be more then 0")]
        public int DeliveryWeight{ get; set; }

        [Range(0, long.MaxValue, ErrorMessage = "Must be more then 0 or equal")]
        public long LocalitySandId{ get; set; }

        [Range(0, long.MaxValue, ErrorMessage = "Must be more then 0 or equal")]
        public long LocalityGetId{ get; set; }
    }
}