using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cBaseQMS.Models
{
    public class TestExpressionViewModel
    {
        #region "Properties"
        public string TestMaster { get; set; }
        public IEnumerable<SelectListItem> TestMasterItems { get; set; }
        public IEnumerable<SelectListItem> ComponentItems { get; set; }       
        public string TestExpJsonValue { get; set; }
        #endregion

        #region "Constants"
        public const string Test = "TST";
        public const string Condition = "CON";
        public const string Equations = "EQN";

        public const string InputField = "INP";
        public const string CalculatedField = "CAL";
        public const string LocationAttribute = "LOA";
        public const string PartAttribute = "PRA";
        #endregion
    }
}