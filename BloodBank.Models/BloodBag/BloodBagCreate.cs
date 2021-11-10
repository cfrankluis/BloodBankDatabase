using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Data;

namespace BloodBank.Models.BloodBag
{
    public class BloodBagCreate
    {
        [Required]
        [EnumDataType(typeof(BloodType))]
        [Display(Name = "Blood Type")]
        public BloodType BloodType { get; set; }

        [Required]
        public double Volume { get; set; }
    }
}
