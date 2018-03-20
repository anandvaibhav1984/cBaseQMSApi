﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cBaseQms.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class cBaseDevEntities : DbContext
    {
        public cBaseDevEntities()
            : base("name=cBaseDevEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<InputFieldsValue> InputFieldsValues { get; set; }
        public virtual DbSet<LocationAttribte> LocationAttribtes { get; set; }
        public virtual DbSet<PartAttribute> PartAttributes { get; set; }
        public virtual DbSet<TestAttribute> TestAttributes { get; set; }
        public virtual DbSet<TestCalculatedField> TestCalculatedFields { get; set; }
        public virtual DbSet<TestInput> TestInputs { get; set; }
        public virtual DbSet<TestMaster> TestMasters { get; set; }
        public virtual DbSet<UoM> UoMs { get; set; }
        public virtual DbSet<LocationMaster> LocationMasters { get; set; }
        public virtual DbSet<PartMaster> PartMasters { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<InputField> InputFields { get; set; }
        public virtual DbSet<Equation> Equations { get; set; }
        public virtual DbSet<AppParameter> AppParameters { get; set; }
        public virtual DbSet<MathConstant> MathConstants { get; set; }
        public virtual DbSet<TestMasterMapping> TestMasterMappings { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<vWLocationAttribute> vWLocationAttributes { get; set; }
        public virtual DbSet<ELMAH_Error> ELMAH_Error { get; set; }
    
        public virtual ObjectResult<usp_GetTestMasterByTestId> usp_GetTestMasterByTestId(Nullable<int> testid)
        {
            var testidParameter = testid.HasValue ?
                new ObjectParameter("Testid", testid) :
                new ObjectParameter("Testid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_GetTestMasterByTestId>("usp_GetTestMasterByTestId", testidParameter);
        }
    
        public virtual int usp_SaveLocationAndPartMapping(Nullable<int> testMasterID, Nullable<int> partMasterID, Nullable<int> locationMasterID, Nullable<bool> isActive, Nullable<int> createdBy, Nullable<System.DateTime> createdOn, Nullable<int> updatedBy, Nullable<System.DateTime> updatedOn)
        {
            var testMasterIDParameter = testMasterID.HasValue ?
                new ObjectParameter("TestMasterID", testMasterID) :
                new ObjectParameter("TestMasterID", typeof(int));
    
            var partMasterIDParameter = partMasterID.HasValue ?
                new ObjectParameter("PartMasterID", partMasterID) :
                new ObjectParameter("PartMasterID", typeof(int));
    
            var locationMasterIDParameter = locationMasterID.HasValue ?
                new ObjectParameter("LocationMasterID", locationMasterID) :
                new ObjectParameter("LocationMasterID", typeof(int));
    
            var isActiveParameter = isActive.HasValue ?
                new ObjectParameter("IsActive", isActive) :
                new ObjectParameter("IsActive", typeof(bool));
    
            var createdByParameter = createdBy.HasValue ?
                new ObjectParameter("CreatedBy", createdBy) :
                new ObjectParameter("CreatedBy", typeof(int));
    
            var createdOnParameter = createdOn.HasValue ?
                new ObjectParameter("CreatedOn", createdOn) :
                new ObjectParameter("CreatedOn", typeof(System.DateTime));
    
            var updatedByParameter = updatedBy.HasValue ?
                new ObjectParameter("UpdatedBy", updatedBy) :
                new ObjectParameter("UpdatedBy", typeof(int));
    
            var updatedOnParameter = updatedOn.HasValue ?
                new ObjectParameter("UpdatedOn", updatedOn) :
                new ObjectParameter("UpdatedOn", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_SaveLocationAndPartMapping", testMasterIDParameter, partMasterIDParameter, locationMasterIDParameter, isActiveParameter, createdByParameter, createdOnParameter, updatedByParameter, updatedOnParameter);
        }
    
        public virtual int usp_UpdateLocationAndPartMapping(Nullable<bool> isActive, Nullable<int> updatedBy, Nullable<System.DateTime> updatedOn, Nullable<int> testMasterMapID)
        {
            var isActiveParameter = isActive.HasValue ?
                new ObjectParameter("IsActive", isActive) :
                new ObjectParameter("IsActive", typeof(bool));
    
            var updatedByParameter = updatedBy.HasValue ?
                new ObjectParameter("UpdatedBy", updatedBy) :
                new ObjectParameter("UpdatedBy", typeof(int));
    
            var updatedOnParameter = updatedOn.HasValue ?
                new ObjectParameter("UpdatedOn", updatedOn) :
                new ObjectParameter("UpdatedOn", typeof(System.DateTime));
    
            var testMasterMapIDParameter = testMasterMapID.HasValue ?
                new ObjectParameter("TestMasterMapID", testMasterMapID) :
                new ObjectParameter("TestMasterMapID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_UpdateLocationAndPartMapping", isActiveParameter, updatedByParameter, updatedOnParameter, testMasterMapIDParameter);
        }
    
        public virtual ObjectResult<usp_GetLocationAndPartMapping> usp_GetLocationAndPartMapping(Nullable<int> testMasterID)
        {
            var testMasterIDParameter = testMasterID.HasValue ?
                new ObjectParameter("TestMasterID", testMasterID) :
                new ObjectParameter("TestMasterID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_GetLocationAndPartMapping>("usp_GetLocationAndPartMapping", testMasterIDParameter);
        }
    }
}
