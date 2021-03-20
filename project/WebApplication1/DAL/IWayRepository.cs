using ComputerNet.DAL.Interfaces;
using DAL.Entity;
using Utils;

namespace DAL
{
    public interface IWayRepository: IGenericRepository<Way>
    {
        Way FindByLocalitySand_IdAndLocalityGet_Id(long localitySandID, long localityGetID);
    }
}