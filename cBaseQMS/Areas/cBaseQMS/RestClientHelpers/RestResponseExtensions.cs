using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cBaseQMS.Areas.RestClientHelpers
{
    public static class RestResponseExtensions
    {
        private static string ResultPropertyName = "Result";

        public static T Extract<T>(this IRestResponse response) where T : new()
        {
            var parsedObj = JObject.Parse(response.Content);
            return JsonConvert.DeserializeObject<T>(parsedObj[ResultPropertyName].ToString());
        }

        //public static T Trim<T>(this IRestResponse response) where T : new()
        //{
        //    response.Content = response.Content.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
        //    return (T)response;
        //}
    }
}