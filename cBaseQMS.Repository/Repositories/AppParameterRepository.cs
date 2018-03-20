using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cBaseQms.DAL;
using cBaseQms.Repository.Infrastructure;

namespace cBaseQms.Repository.Repositories
{
    public class AppParameterRepository: RepositoryBase<AppParameter>, IAppParameterRepository
    {
        public AppParameterRepository(IDbFactory dbFactory) : base(dbFactory) { }
        public string GetAppParamValue(string paramName)
        {
            var appParam = this.DbContext.AppParameters.Where(x => x.ParamName == paramName).FirstOrDefault();
            return appParam.ParamValue; 
        }

        public bool IsActiveAppParam(string paramName)
        {
            var appParam = this.DbContext.AppParameters.Where(x => x.ParamName == paramName).FirstOrDefault();
            return appParam.IsActive;
        }

        public AppParameter GetAppParam(int id)
        {
            var appParam = this.DbContext.AppParameters.Where(x => x.AppParamID == id).FirstOrDefault();
            return appParam;
        }

        public List<AppParameter> GetParameters()
        {
            var appParam = this.DbContext.AppParameters.ToList();
            return appParam;
        }

        public bool SaveAppParameters(AppParameter appParam)
        {
            appParam.CreatedOn = DateTime.Now;
            base.Add(appParam);
            return true;
        }

        public bool UpdateAppParameters(AppParameter appParam)
        {
            appParam.ModifiedOn = DateTime.Now;
            base.Update(appParam);
            return true;
        }
    }

    public interface IAppParameterRepository:IRepository<AppParameter>
    {
        string GetAppParamValue(string paramName);
        bool IsActiveAppParam(string paramName);
        AppParameter GetAppParam(int id);
        List<AppParameter> GetParameters();
        bool SaveAppParameters(AppParameter appParam);
        bool UpdateAppParameters(AppParameter appParam);
    }
}
