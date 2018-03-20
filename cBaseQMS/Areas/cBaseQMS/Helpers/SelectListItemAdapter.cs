using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cBaseQMS.Areas.cBaseQMS.Helpers
{
    public static class SelectListItemAdapter
    {

        public static IEnumerable<SelectListItem> GetSelectListItemCollection<T>

            (IEnumerable<T> source, Func<T, string> text, Func<T, string> value,string selected="", bool createEmpty = false) where T : class

        {
            var selectListItems = new List<SelectListItem>();
            if (createEmpty)
            {
                selectListItems.Add(new SelectListItem { Text = "Please Select", Value = "", Selected = true });
            }
            foreach (var item in source)
            {
                int outSelected;
                if (int.TryParse(selected, out outSelected))
                {
                    selectListItems.Add(new SelectListItem { Text = text(item), Value = value(item), Selected = (value(item) == selected) });
                }
                else
                {
                    selectListItems.Add(new SelectListItem { Text = text(item), Value = value(item)});
                }
                
            }

            return selectListItems;

        }

       

    }
}