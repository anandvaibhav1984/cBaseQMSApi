using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;
using cBaseQms.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cBaseQms.Service.Services
{
    public class AppParameterService:IAppParameterService
    {
        private readonly IAppParameterRepository appParameterRepository;
        private readonly IUnitOfWork unitOfWork;

        public AppParameterService(IAppParameterRepository appParameterRepository,IUnitOfWork unitOfWork)
        {
            this.appParameterRepository = appParameterRepository;
            this.unitOfWork = unitOfWork;
        }


        string IAppParameterService.GetAppParamValue(string paramName)
        {
            return paramName;
        }

        bool IAppParameterService.IsActiveAppParam(string paramName)
        {
            return appParameterRepository.IsActiveAppParam(paramName);
           
        }

        AppParameter IAppParameterService.GetAppParam(int id)
        {
            return appParameterRepository.GetAppParam(id);
        }

        List<AppParameter> IAppParameterService.GetParameters()
        {
            return appParameterRepository.GetParameters();
        }

        bool IAppParameterService.SaveAppParameters(AppParameter appParam)
        {
            return appParameterRepository.SaveAppParameters(appParam);

        }

        bool IAppParameterService.UpdateAppParameters(AppParameter appParam)
        {
            return appParameterRepository.UpdateAppParameters(appParam);
        }

        void IAppParameterService.CommitAppParam()
        {
            unitOfWork.Commit();
        }
    }

    public interface IAppParameterService
    {
        string GetAppParamValue(string paramName);
        bool IsActiveAppParam(string paramName);
        AppParameter GetAppParam(int id);
        List<AppParameter> GetParameters();
        bool SaveAppParameters(AppParameter appParam);
        bool UpdateAppParameters(AppParameter appParam);
        void CommitAppParam();
    }
}
