using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IbreastCare.ViewModel
{
    public class SymptomDetailViewModel
    {
        public int SymptomDetailID { get; set; }
        public int SymptomID { get; set; }
        public int SeverityLevelID { get; set; }
        public string Description { get; set; }
    }
}