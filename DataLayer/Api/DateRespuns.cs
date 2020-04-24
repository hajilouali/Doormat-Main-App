using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Api
{
    public class DateRespuns
    {
        public typeDateRespuns type { get; set; }
        public List<values> values { get; set; }
    }
    public enum typeDateRespuns
    {
        success,
        failed
    }
    public class values
    {
        public int id { get; set; }
        public string type { get; set; }
        public string category { get; set; }
        public string occasion { get; set; }
    }
}
