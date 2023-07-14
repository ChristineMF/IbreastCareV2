using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IbreastCare.Models
{
    public class BWDTO
    {

            public int BWId { get; set; }
            public Nullable<double> BW1 { get; set; }
            public Nullable<double> BMI { get; set; }
            public DateTime InputDate { get; set; }
            public DateTime? MeasureDate { get; set; }
       
    }
}