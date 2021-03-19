using System.Collections.Generic;
using BLL.dto;
using BLL.Intarfaces;
using BLL.mapers.impl;
using DAL;

namespace BLL.impl
{
    public class LocalityService : ILocalityService
    {
        private ILocalityRepository _localityRepository;

        public LocalityService(ILocalityRepository localityRepository)
        {
            this._localityRepository = localityRepository;
        }

        public List<LocalityModel> getLocalities()
        {
            return LocalityToLocalityModelMapper.mapToList(_localityRepository.Get());
        }

        public List<LocalityModel> findGetLocalitiesByLocalitySetId(long id)
        {
            return LocalityToLocalityModelMapper.mapToList(_localityRepository.findGetLocalitiesByLocalitySandId(id));
        }
    }
}