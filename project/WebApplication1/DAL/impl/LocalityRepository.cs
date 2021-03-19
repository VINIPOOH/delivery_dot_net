using System.Collections.Generic;
using System.Linq;
using ComputerNet.DAL.Repositories;
using DAL.Entity;
using WebApplication1.Dal;

namespace DAL.impl
{
    public class LocalityRepository:GenericRepository<Locality>, ILocalityRepository
    {
        public LocalityRepository(MyDbContext context) : base(context)
        {
        }

        public IEnumerable<Locality> findGetLocalitiesByLocalitySandId(long idToSearch)
        {
            return base.Get(locality => locality.WaysWhereThisLocalityIsGet
                .Exists(way => way.LocalitySandLocalityId.Equals(idToSearch)));
        }
    }
}