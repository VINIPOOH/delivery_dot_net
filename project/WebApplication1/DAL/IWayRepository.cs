using ComputerNet.DAL.Interfaces;
using DAL.Entity;
using Utils;

namespace DAL
{
    public interface IWayRepository: IGenericRepository<Way>
    {
        Way findByLocalitySand_IdAndLocalityGet_Id(long localitySandID, long localityGetID);
    }
}