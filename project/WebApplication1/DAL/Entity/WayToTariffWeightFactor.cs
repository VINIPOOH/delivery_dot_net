using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class WayToTariffWeightFactor
    {
        public long WayToTariffWeightFactorId{ get; set; }
        
        public long WayId { get; set; }
        
        public Way Way { get; set; }
        
        public long TariffWeightFactorId { get; set; }
        
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