using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Data;


namespace BloodBank.Models.PatientModels
{
    public class PatientCreate
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Blood Type")]
        [EnumDataType(typeof(BloodType))]
        public BloodType BloodType { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
