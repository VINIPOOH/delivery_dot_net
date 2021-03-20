using System.Collections.Generic;
using ComputerNet.DAL.Interfaces;
using DAL.Entity;

namespace DAL
{
    public interface ILocalityRepository:IGenericRepository<Locality>
    {
        IEnumerable<Locality> FindGetLocalitiesByLocalitySandId(long idToSearch);
    }
}