using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Data;

namespace BloodBank.Models.BloodBag
{
    public class BloodBagDetail
    {
        [Display(Name = "ID")]
        public int BloodID { get; set; }

        [Display(Name = "BloodType")]
        [EnumDataType(typeof(BloodType))]
        public BloodType BloodType { get; set; }
        public double Volume { get; set; }

        [Display(Name = "Days Stored")]
        public double DaysStored { get; set; }

        [Display(Name = "Extraction Date")]
        [DataType(DataType.Date)]
        public DateTime ExtractionDate { get; set; }

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