using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BloodBank.Data;

namespace BloodBank.Models.BloodOrder
{
    public class BloodOrderListItem
    {
        [Display(Name = "Order ID")]
        public int ID { get; set; }

        [Display(Name = "Blood Type")]
        public BloodType BloodType { get; set; }

        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

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
