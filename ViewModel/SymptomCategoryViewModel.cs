using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IbreastCare.ViewModel
{
    public class SymptomCategoryViewModel
    {
        public int UserId { get; set; }
        public int SymptomID { get; set; }
        public string Category { get; set; }
       
    }
}