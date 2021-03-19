using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.dto
{
    public class DeliveryOrderCreateModel
    {
        public DeliveryOrderCreateModel(int deliveryWeight, long localitySandId, long localityGetId, string addresseeEmail)
        {
            DeliveryWeight = deliveryWeight;
            LocalitySandId = localitySandId;
            LocalityGetId = localityGetId;
            AddresseeEmail = addresseeEmail;
        }

        [Required(ErrorMessage = "Must be not empty")]
        [Range(1, int.MaxValue, ErrorMessage = "Must be more then 0")]
        public int DeliveryWeight{ get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Must be more then 0 or equal to 0")]
        public long LocalitySandId{ get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Must be more then 0 or equal to 0")]
        public long LocalityGetId{ get; set; }
        [Required(ErrorMessage = "Must be not empty")]
        [EmailAddress]
        public String AddresseeEmail{ get; set; }
    }
}