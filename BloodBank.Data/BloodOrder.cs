using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank.Data
{
    public class BloodOrder
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public double Amount { get; set; }
        
        [Required]
        public BloodType BloodType { get; set; }

        [ForeignKey(nameof(Patient))]
        public int PatientID { get; set; }
        public virtual Patient Patient { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
