using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace IbreastCare.ViewModel
{
    public class BodyWeightViewModel
    {
        public int BWId { get; set; }
        [DisplayName("請輸入體重")]
        public Nullable<double> BW1 { get; set; }
        [DisplayName("BMI")]
        public Nullable<double> BMI { get; set; }
        public string InputDate { get; set; }
        public int MyId { get; set; }
        [DisplayName("測量日期")]
        public DateTime MeasureDate { get; set; }
    }
}