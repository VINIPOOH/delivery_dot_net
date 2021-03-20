using System.Collections.Generic;
using System.Linq;
using ComputerNet.DAL.Repositories;
using DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Utils;
using WebApplication1.Dal;

namespace DAL.impl
{
    public class WayRepository : GenericRepository<Way>, IWayRepository
    {
        public WayRepository(MyDbContext context) : base(context)
        {
        }

        public Way FindByLocalitySand_IdAndLocalityGet_Id(long localitySandID, long localityGetID)
        {
            return Context.Ways.Include(p => p.WayToTariffWeightFactors).ThenInclude(factor =>
                factor.TariffWeightFactor).FirstOrDefault(way => way.LocalitySandLocalityId.Equals(localitySandID) &&
                                                                 way.LocalityGetLocalityId.Equals(localityGetID));
        }
    }
}