using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IbreastCare.ViewModel
{
    public class SymptomViewModel
    {
        public int SymptomID { get; set; }
        public IEnumerable<SelectListItem> Category { get; set; }
        public string SymptomName { get; set; }
        public int SeverityLevelID { get; set; }
        public string SeverityLevel { get; set; }
        
        public string Description { get; set; }
        public int UserID { get; set; }
    }
}