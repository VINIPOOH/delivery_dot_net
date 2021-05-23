using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class TariffWeightFactor
    {
        public long TariffWeightFactorId{ get; set; }
        public int MinWeightRange{ get; set; }
        public int MaxWeightRange{ get; set; }
        public int OverPayOnKilometer{ get; set; }
        public List<WayToTariffWeightFactor> WayToTariffWeightFactors { get; set; }

        public TariffWeightFactor()
        {
        }

        public TariffWeightFactor(long tariffWeightFactorId, int minWeightRange, int maxWeightRange, int overPayOnKilometer)
        {
            TariffWeightFactorId = tariffWeightFactorId;
            MinWeightRange = minWeightRange;
            MaxWeightRange = maxWeightRange;
            OverPayOnKilometer = overPayOnKilometer;
        }
    }
}