using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entity
{
    public class Way
    {
        public List<Delivery> Deliveries { get; set; }
         public long LocalitySandLocalityId { get; set; }
         public Locality LocalitySand { get; set; }
         public long LocalityGetLocalityId { get; set; }
         public Locality LocalityGet { get; set; }
        public List<WayToTariffWeightFactor> WayToTariffWeightFactors { get; set; }
         public long WayId { get; set; }
         public int DistanceInKilometres { get; set; }
         public int TimeOnWayInDays { get; set; }
         public int PriceForKilometerInCents { get; set; }

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
        
        public Way(long localitySandLocalityId, Locality localitySand, long localityGetLocalityId, Locality localityGet, int distanceInKilometres, int timeOnWayInDays, int priceForKilometerInCents)
        {
            LocalitySandLocalityId = localitySandLocalityId;
            LocalitySand = localitySand;
            LocalityGetLocalityId = localityGetLocalityId;
            LocalityGet = localityGet;
            DistanceInKilometres = distanceInKilometres;
            TimeOnWayInDays = timeOnWayInDays;
            PriceForKilometerInCents = priceForKilometerInCents;
        }
    }
}