using System.Collections.Generic;
using BLL.dto;
using DAL.Entity;

namespace BLL.mapers.impl
{
    public class LocalityToLocalityModelMapper
    {
        public static LocalityModel map(Locality locality)
        {
            return new LocalityModel(locality.LocalityId, locality.NameEn);
        }

        public static List<LocalityModel> mapToList(IEnumerable<Locality> entities)
        {
            List<LocalityModel> dtos = new List<LocalityModel>();
            foreach (var locality in entities)
            {
                dtos.Add(map(locality));
            }

            return dtos;
        }
    }
}