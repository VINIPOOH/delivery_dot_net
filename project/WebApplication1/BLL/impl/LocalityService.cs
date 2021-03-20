using System.Collections.Generic;
using BLL.dto;
using BLL.Intarfaces;
using BLL.mapers.impl;
using DAL;

namespace BLL.impl
{
    public class LocalityService : ILocalityService
    {
        private readonly ILocalityRepository _localityRepository;

        public LocalityService(ILocalityRepository localityRepository)
        {
            _localityRepository = localityRepository;
        }

        public List<LocalityModel> GetLocalities()
        {
            return LocalityToLocalityModelMapper.mapToList(_localityRepository.Get());
        }

        public List<LocalityModel> FindGetLocalitiesByLocalitySetId(long id)
        {
            return LocalityToLocalityModelMapper.mapToList
                (_localityRepository.FindGetLocalitiesByLocalitySandId(id));
        }
    }
}