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
    public interface ILocationMasterService
    {
        IEnumerable<LocationMasterViewModel> GetAllLocations();
       
    }
    public class LocationMasterService : ILocationMasterService
    {
        private readonly ILocationMasterRepositry locationMasterRepositry;        
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _iMapper;



        public LocationMasterService(ILocationMasterRepositry locationMasterRepositry, IUnitOfWork unitOfWork, IMapper _iMapper)
        {   
            this.locationMasterRepositry = locationMasterRepositry;
            this.unitOfWork = unitOfWork;
            this._iMapper = _iMapper;

        }
        public IEnumerable<LocationMasterViewModel> GetAllLocations()
        {
            List<LocationMasterViewModel> locationMasterViewModel;
            List<LocationMaster> locationMaster;

            locationMaster= locationMasterRepositry.GetAll().ToList();
            locationMasterViewModel = _iMapper.Map<List<LocationMaster>, List<LocationMasterViewModel>>(locationMaster);
            return locationMasterViewModel;
        }


    }
}
