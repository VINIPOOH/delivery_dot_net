﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class ApartmentModel
    {
        public ApartmentModel()
        {
        }

        public ApartmentModel(int apartmentId, int apartmentNumber, int apartmentSpace, int houseId, List<UserModel> userApartments)
        {
            ApartmentId = apartmentId;
            ApartmentNumber = apartmentNumber;
            ApartmentSpace = apartmentSpace;
            HouseId = houseId;
        }

        public int ApartmentId { get; set; }
        [Required] public int ApartmentNumber { get; set; }
        [Required] public int ApartmentSpace { get; set; }
        
        public int HouseId{ get; set; }
    }
}