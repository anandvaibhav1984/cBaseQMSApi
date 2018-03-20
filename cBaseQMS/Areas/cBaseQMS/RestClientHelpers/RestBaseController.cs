using cBaseQMS.Areas.RestClientHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Web.Mvc;

namespace cBaseQMS.Areas.RestController
{
    public class RestClientBase : RestClient, IRestClientBase
    {
        public ModelStateDictionary MODELSTATE { set { this._modelstate = value; } }
        public ModelStateDictionary _modelstate;

        public RestClientBase() : base(Constants.ApiUrl)
        {
        }

        public new void Execute(IRestRequest request)
        {
            var response = base.Execute(request);
            var parsedObj = JObject.Parse(response.Content);
            var apiResponse = JsonConvert.DeserializeObject<ResponsePackage>(parsedObj.ToString());
            if (apiResponse.HasErrors)
            {
                AddErrors(apiResponse);
            }
        }

        public new T Execute<T>(IRestRequest request) where T : new()
        {
            var response = base.Execute(request);
            var parsedObj = JObject.Parse(response.Content);
            var apiResponse = JsonConvert.DeserializeObject<ResponsePackage>(parsedObj.ToString());
            if (apiResponse.HasErrors)
            {
                AddErrors(apiResponse);
                return default(T);
            }
            return response.Extract<T>();
        }

        private void AddErrors(ResponsePackage response)
        {
            List<string> listMessagesAdded = new List<string>();
            for (int i = 0; i < response.Errors.Count; i++)
            {
                if (listMessagesAdded.Contains(response.Errors[i])) continue;
                _modelstate.AddModelError(i.ToString(), response.Errors[i]);
                listMessagesAdded.Add(response.Errors[i]);
            }
        }
    }

    public interface IRestClientBase
    {
        ModelStateDictionary MODELSTATE { set; }

        void Execute(IRestRequest request);

        T Execute<T>(IRestRequest request) where T : new();
    }
}