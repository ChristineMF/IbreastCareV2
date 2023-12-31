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
    
    public partial class Member
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Member()
        {
            this.BWs = new HashSet<BW>();
            this.Personal_Datas = new HashSet<Personal_Datas>();
            this.MySymptoms = new HashSet<MySymptom>();
        }
    
        public int UserId { get; set; }
        public string RealName { get; set; }
        public string NickName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public int RoleId { get; set; }
        public System.DateTime InputDate { get; set; }
        public string Status { get; set; }
        public string SettingUser { get; set; }
        public Nullable<System.DateTime> SettingDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BW> BWs { get; set; }
        public virtual Role Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Personal_Datas> Personal_Datas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MySymptom> MySymptoms { get; set; }
    }
}
