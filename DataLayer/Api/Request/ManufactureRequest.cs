using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Api.Request
{
    public class ManufactureRequest
    {
        public bool AllTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
