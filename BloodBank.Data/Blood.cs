using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank.Data
{
    public class Blood
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public double Volume { get; set; }

        [Required]
        public DateTime ExtractionDate { get; set; }

        [ForeignKey(nameof(Donor))]
        public Guid DonorId { get; set; }
        public virtual Donor Donor { get; set; }

        [ForeignKey(nameof(BloodTable))]
        public BloodType BloodType { get { return Donor.BloodType; } private set { } }
        public virtual BloodTable BloodTable { get; set; }

        public int DaysStored { get { return (ExtractionDate - DateTime.Now).Days; } private set { } }
    }
}
