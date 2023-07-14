using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IbreastCare.Models
{
    public class BWDataResult
    {
        public string DataMeasure { get; set; }
        public string DataBW { get; set; }
        public string DataBMI { get; set; }
        public IEnumerable<BWDTO> BWList { get; set; }
    }
}