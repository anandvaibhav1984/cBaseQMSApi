using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace cBaseQMS.Areas.cBaseQMS.Helpers
{
    public static class QueryStringHelper
    {
        public static Dictionary<string, string> getUrlParam(this string query, HttpContextBase httpContext)
        {
            System.Collections.Specialized.NameValueCollection coll = System.Web.HttpUtility.ParseQueryString(httpContext.Request.UrlReferrer.Query);
            Dictionary<string, string> keyValuePair = new Dictionary<string, string>();
            for (int i = 0; i < coll.Count; i++)
            {
                keyValuePair.Add(coll.GetKey(i), coll.Get(i));
            }
            return keyValuePair;

        }

      
    }
}