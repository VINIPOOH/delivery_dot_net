using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class Way
    {
        public List<Delivery> Deliveries { get; set; }
        [Required] public long LocalitySandLocalityId { get; set; }
        [Required] public Locality LocalitySand { get; set; }
        [Required] public long LocalityGetLocalityId { get; set; }
        [Required] public Locality LocalityGet { get; set; }
        public List<WayToTariffWeightFactor> WayToTariffWeightFactors { get; set; }
        [Required] public long WayId { get; set; }
        [Required] public int DistanceInKilometres { get; set; }
        [Required] public int TimeOnWayInDays { get; set; }
        [Required] public int PriceForKilometerInCents { get; set; }

        public Way()
        {
        }

        public Way(long localitySandLocalityId, Locality localitySand, long localityGetLocalityId, Locality localityGet,
            long wayId, int distanceInKilometres, int timeOnWayInDays, int priceForKilometerInCents,
            List<WayToTariffWeightFactor> wayToTariffWeightFactors)
        {
            LocalitySandLocalityId = localitySandLocalityId;
            LocalitySand = localitySand;
            LocalityGetLocalityId = localityGetLocalityId;
            LocalityGet = localityGet;
            WayId = wayId;
            DistanceInKilometres = distanceInKilometres;
            TimeOnWayInDays = timeOnWayInDays;
            PriceForKilometerInCents = priceForKilometerInCents;
            WayToTariffWeightFactors = wayToTariffWeightFactors;
        }
    }
}