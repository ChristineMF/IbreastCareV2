using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IbreastCare.ViewModel
{
    public class MySymptomViewModel
    {
        public int MySymptomsId { get; set; }
        public int UserId { get; set; }
        public int SymptomDetailID { get; set; }
        public System.DateTime OnsetDate { get; set; }
        public System.DateTime InputDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        
    }
}