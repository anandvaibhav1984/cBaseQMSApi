using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using cBaseQMS.Service.Models;
using cBaseQms.DAL;

namespace cBaseQms.Service.AutoMapperConfig
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<TestMaster, TestMasterViewModel>();
            CreateMap<TestMasterViewModel, TestMaster>();
            CreateMap<Test, TestsViewModel>();
           // CreateMap<TestsViewModel, Test>();

            CreateMap<TestsViewModel, Test>()
                                                       .AfterMap((src, dest) =>
                                                       {
                                                           if (src.TestID < 1)
                                                           {
                                                               dest.CreatedOn = DateTime.Now;
                                                               dest.CreatedBy = 1111;
                                                               dest.IsActive = true;
                                                           }
                                                           else if (src.Operation == "Update")
                                                           {
                                                               dest.ModifiedOn = DateTime.Now;
                                                               dest.ModifiedBy = 1111;
                                                               dest.IsActive = true;
                                                           }
                                                           else if (src.Operation == "Delete")
                                                           {
                                                               dest.ModifiedOn = DateTime.Now;
                                                               dest.ModifiedBy = 1111;
                                                               dest.IsActive = false;

                                                           }
                                                           else if (src.Operation == "activate")
                                                           {
                                                               dest.ModifiedOn = DateTime.Now;
                                                               dest.ModifiedBy = 1111;
                                                               if (src.IsActive)
                                                                   dest.IsActive = false;
                                                               else
                                                                   dest.IsActive = true;



                                                           }

                                                       });

            CreateMap<TestMasterViewModel, TestMaster>()
                                                       .AfterMap((src, dest) =>
                                                       {
                                                           if (src.TestMasterID < 1)
                                                           {
                                                               dest.CreatedOn = DateTime.Now;
                                                               dest.CreatedBy = 1111;
                                                               dest.IsActive = true;
                                                           }
                                                           else if (src.operation == "Update")
                                                           {
                                                               dest.ModifiedOn = DateTime.Now;
                                                               dest.ModifiedBy = 1111;
                                                               dest.IsActive = true;
                                                           }
                                                           else if (src.operation == "Delete")
                                                           {
                                                               dest.ModifiedOn = DateTime.Now;
                                                               dest.ModifiedBy = 1111;
                                                               dest.IsActive = false;

                                                           }
                                                       });

            CreateMap<TestMasterMappingViewModel, TestMasterMapping>()
                                                       .AfterMap((src, dest) =>
                                                       {
                                                           if (src.TestMasterMapID < 1)
                                                           {
                                                               dest.CreatedOn = DateTime.Now;
                                                               dest.CreatedBy = 1111;
                                                               dest.IsActive = true;
                                                           }
                                                           else if (src.operation == "Remove")
                                                           {
                                                               dest.ModifiedOn = DateTime.Now;
                                                               dest.ModifiedBy = 1111;
                                                               dest.IsActive = false;
                                                           }



                                                       });
           // CreateMap<TestMasterMappingViewModel, TestMasterMappingViewModel>(MemberList.Source);
        }
    }
}