using System.Collections.Generic;
using BLL.dto;

namespace BLL.Intarfaces
{
    public interface ILocalityService
    {
        List<LocalityModel> GetLocalities();

        List<LocalityModel> FindGetLocalitiesByLocalitySetId(long id);
    }
}