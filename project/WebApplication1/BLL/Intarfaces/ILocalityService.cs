using System.Collections.Generic;
using BLL.dto;

namespace BLL.Intarfaces
{
    public interface ILocalityService
    {
        List<LocalityModel> getLocalities();

        List<LocalityModel> findGetLocalitiesByLocalitySetId(long id);
    }
}