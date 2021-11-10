using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank.Data
{
    public class BloodBag
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Range(0,double.PositiveInfinity)]
        public double Volume { get; set; }

        [Required]
        public DateTime ExtractionDate { get; set; }

/*        [ForeignKey(nameof(Donor))]
        public Guid DonorId { get; set; }
        public virtual Donor Donor { get; set; }*/


        [ForeignKey(nameof(BloodTable))]
        public BloodType BloodType { get; set; }
        public virtual BloodTable BloodTable { get; set; }

        public double DaysStored 
        { get 
            { 
                double days = (DateTime.Now - ExtractionDate).TotalDays;
                return days > 1 ? Math.Round(days, 2) : 0;
            } 
            private set { } 
        }
    }
}
