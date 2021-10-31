using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Data;

namespace BloodBank.Models.Patient
{
    public class PatientListItem
    {
        public int PatientID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        [Display(Name = "Blood Type")]
        [EnumDataType(typeof(BloodType))]
        public BloodType BloodType { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check in Date")]
        public DateTime CheckInDate { get; set; }

        public string BloodValue
        {
            get
            {
                switch (BloodType)
                {
                    case BloodType.Ap:
                        return "A+";
                    case BloodType.An:
                        return "A-";
                    case BloodType.Bp:
                        return "B+";
                    case BloodType.Bn:
                        return "B-";
                    case BloodType.ABp:
                        return "AB+";
                    case BloodType.ABn:
                        return "AB-";
                    case BloodType.Op:
                        return "O+";
                    case BloodType.On:
                        return "O-";
                    default:
                        return "Other";
                }
            }
            private set { }
        }
    }
}
