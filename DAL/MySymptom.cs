//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IbreastCare.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class MySymptom
    {
        public int MySymptomsId { get; set; }
        public int UserId { get; set; }
        public Nullable<int> SymptomDetailID { get; set; }
        public System.DateTime OnsetDate { get; set; }
        public System.DateTime InputDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
    
        public virtual Member Member { get; set; }
        public virtual SymptomDetail SymptomDetail { get; set; }
    }
}