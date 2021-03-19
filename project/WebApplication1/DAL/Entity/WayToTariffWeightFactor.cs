using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class WayToTariffWeightFactor
    {
        public long WayToTariffWeightFactorId{ get; set; }
        [Required]
        public long WayId { get; set; }
        [Required]
        public Way Way { get; set; }
        [Required]
        public long TariffWeightFactorId { get; set; }
        [Required]
        public TariffWeightFactor TariffWeightFactor { get; set; }

        public WayToTariffWeightFactor(long wayToTariffWeightFactorId, long tariffWeightFactorId, TariffWeightFactor tariffWeightFactor)
        {
            WayToTariffWeightFactorId = wayToTariffWeightFactorId;
            TariffWeightFactorId = tariffWeightFactorId;
            TariffWeightFactor = tariffWeightFactor;
        }

        public WayToTariffWeightFactor()
        {
        }
    }
}