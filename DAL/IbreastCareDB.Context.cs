﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IbreastDBEntities : DbContext
    {
        public IbreastDBEntities()
            : base("name=IbreastDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BW> BWs { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MyOperation> MyOperations { get; set; }
        public virtual DbSet<MyTreat> MyTreats { get; set; }
        public virtual DbSet<OperationType> OperationTypes { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Personal_Datas> Personal_Datas { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SymptomDetail> SymptomDetails { get; set; }
        public virtual DbSet<Symptom> Symptoms { get; set; }
        public virtual DbSet<TreatPlan> TreatPlans { get; set; }
        public virtual DbSet<SeverityLevel> SeverityLevels { get; set; }
        public virtual DbSet<MySymptom> MySymptoms { get; set; }
    }
}
