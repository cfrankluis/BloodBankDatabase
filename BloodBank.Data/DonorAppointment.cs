using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank.Data
{
    public class DonorAppointment
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [ForeignKey(nameof(Donor))]
        public Guid DonorID { get; set; }
        public virtual Donor Donor { get; set; }

    }
}
