using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Data;

namespace BloodBank.Models.PatientModels
{
    public class PatientEdit
    {
        [Required]
        public int PatientID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EnumDataType(typeof(BloodType))]
        [Display(Name = "Blood Type")]
        public BloodType BloodType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
    }
}
