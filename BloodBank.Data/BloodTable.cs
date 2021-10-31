using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank.Data
{
    public enum BloodType
    {
        [Display(Name = "A+")]
        Ap,
        [Display(Name = "A-")]
        An,
        [Display(Name = "B+")]
        Bp,
        [Display(Name = "B-")]
        Bn,
        [Display(Name = "AB+")]
        ABp,
        [Display(Name = "AB-")]
        ABn,
        [Display(Name = "O+")]
        Op,
        [Display(Name = "O-")]
        On,
    }

    public class BloodTable
    {
        [Key]
        [Range(0,7)]
        public BloodType Type { get; set; }

        public virtual IEnumerable<Patient> Patients { get; set; } = new List<Patient>();
        public virtual IEnumerable<Donor> Donors { get; set; } = new List<Donor>();
        public virtual IEnumerable<Blood> Inventory { get; set; } = new List<Blood>();
    }
}
