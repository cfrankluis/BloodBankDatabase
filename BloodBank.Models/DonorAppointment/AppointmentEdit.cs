using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank.Models.DonorAppointment
{
    public class AppointmentEdit
    {
        [Required]
        public int AppointmentID { get; set; }
        [Required]
        public Guid DonorId { get; set; }
        [Required]
        public DateTime AppointmentTime { get; set; }
    }
}
