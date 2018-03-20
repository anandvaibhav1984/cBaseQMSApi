using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using cBaseQms.Repository;
using cBaseQms.Repository.Repositories;
using cBaseQms.Repository.Infrastructure;
using cBaseQMS.Service.Models;
using AutoMapper;

namespace cBaseQms.Service.Services
{
    // operations you want to expose
    public interface IPartMasterService
    {
        IEnumerable<PartMasterViewModel> GetAllParts();
        
    }
    public class PartMasterService : IPartMasterService
    {
        private readonly IPartMasterRepositry partMasterRepositry;
        private readonly IMapper _iMapper;


        public PartMasterService(IPartMasterRepositry partMasterRepositry, IMapper _iMapper)
        {   
            this.partMasterRepositry = partMasterRepositry;
            this._iMapper = _iMapper;

        }
        public IEnumerable<PartMasterViewModel> GetAllParts()
        {
            IEnumerable<PartMasterViewModel> partMasterViewModel;
            IEnumerable<PartMaster> testMaster;
            testMaster= partMasterRepositry.GetAll().ToList();
            partMasterViewModel = _iMapper.Map<IEnumerable<PartMaster>, IEnumerable<PartMasterViewModel>>(testMaster);
            return partMasterViewModel;
        }

        
    }
}
