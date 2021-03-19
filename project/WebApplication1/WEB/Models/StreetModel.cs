﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class StreetModel
    {

        public StreetModel()
        {
        }

        public StreetModel(int id, string name, ICollection<HouseModel> houses, int cityId)
        {
            Id = id;
            Name = name;
            CityId = cityId;
        }

        public int Id { get; set; }
        [Required] public string Name { get; set; }
        public int CityId { get; set; }
    }
}