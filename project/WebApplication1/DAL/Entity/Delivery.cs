using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class Delivery
    {
        public Bill Bill { get; set; }
        [Required]
        public long DeliveryId{ get; set; }
        [Required]
        public long WayId { get; set; }
        [Required]
        public Way Way { get; set; }
        [Required]
        public string AddresseeUserId { get; set; }
        [Required]
        public User Addressee { get; set; }
        public bool IsPackageReceived{ get; set; }
        [Required]
        public int Weight{ get; set; }

        public Delivery()
        {
        }

        public Delivery(long deliveryId, long wayId, Way way, string addresseeUserId, User addressee, bool isPackageReceived, int weight)
        {
            DeliveryId = deliveryId;
            WayId = wayId;
            Way = way;
            AddresseeUserId = addresseeUserId;
            Addressee = addressee;
            IsPackageReceived = isPackageReceived;
            Weight = weight;
        }
    }
}