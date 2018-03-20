using System.Collections.Generic;

namespace cBaseQMS.Areas.cBaseQMS.Helpers
{
    public class Alert
    {
        public string AlertClass { get; private set; }
        public List<string> Message { get; private set; }

        public Alert(string alertClass, List<string> message)
        {
            AlertClass = alertClass;
            Message = message;
        }
    }
}