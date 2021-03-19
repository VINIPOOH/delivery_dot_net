﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class CityModel
    {
        public CityModel(string name)
        {
            Name = name;
        }

        public CityModel()
        {
        }

        public CityModel(int id, string name, ICollection<StreetModel> streets)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        [Required] public string Name { get; set; }
    }
}