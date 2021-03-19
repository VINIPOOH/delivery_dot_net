using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

namespace WEB.Models
{
    public class CityPageModel
    {
        public string SityName { get; set; }
        
        public int AmountSreets { get; set; }
        
        public string StreatName { get; set; }
    }
}