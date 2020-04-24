using DataLayer.Api.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Api.Request
{
    public class GetFactor
    {
        public bool ISAllType { get; set; }
        public FactorType FactorType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int ClientID { get; set; }
    }
}
