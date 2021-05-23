using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class Locality
    {
        public long LocalityId{ get; set; }
        public string NameEn{ get; set; }
        
        [InverseProperty("LocalitySand")]
        public List<Way> WaysWhereThisLocalityIsSend{ get; set; }
        [InverseProperty("LocalityGet")]
        public List<Way> WaysWhereThisLocalityIsGet{ get; set; }

        public Locality()
        {
        }

        public Locality(long localityId, string nameEn)
        {
            LocalityId = localityId;
            NameEn = nameEn;
        }
    }
}