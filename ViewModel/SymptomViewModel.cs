using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IbreastCare.ViewModel
{
    public class SymptomViewModel
    {
        public int UserId { get; set; }
        public int SymptomID { get; set; }
        public string Category { get; set; }
        public int SeverityLevelID { get; set; }
   
        public string SeverityLevelName { get; set; }


        public int SymptomDetailID { get; set; }
        public string Description { get; set; }
        

        public int MySymptomsId { get; set; }
        public System.DateTime OnsetDate { get; set; }
        public System.DateTime InputDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
    }
}