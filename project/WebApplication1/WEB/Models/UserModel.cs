﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class UserModel
    {
        public UserModel()
        {
        }

        public UserModel(int userId, string fIO, DateTime bornDate, List<ApartmentModel> userApartments)
        {
            UserId = userId;
            FIO = fIO;
            BornDate = bornDate;
        }

        public int UserId { get; set; }
        [Required]
        public string FIO { get; set; }
        [Required]
        public DateTime BornDate { get; set; }
    }
}