using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BloodBank.Data;

namespace BloodBank.Models.BloodOrder
{
    public class BloodOrderCreate
    {
        [Required]
        [Display(Name = "Patient")]
        public int PatientID { get; set; }

        [Required]
        [Display(Name = "Blood Type")]
        public BloodType BloodType { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public double Amount { get; set; }

    }
}
