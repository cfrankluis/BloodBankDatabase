using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Data;

namespace BloodBank.Models.BloodBag
{
    public class BloodBagEdit
    {
        [Required]
        [Display(Name = "ID")]
        public int BloodID { get; set; }

        [Required]
        [EnumDataType(typeof(BloodType))]
        [Display(Name = "Blood Type")]
        public BloodType BloodType { get; set; }

        [Required]
        public double Volume { get; set; }

/*        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Extraction Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ExtractionDate { get; set; }*/




    }
}
