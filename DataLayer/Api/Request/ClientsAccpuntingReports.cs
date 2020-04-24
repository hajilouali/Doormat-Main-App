using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Api.Request
{
    public class ClientsAccpuntingReports
    {
        public int ClientID { get; set; }
        public bool Bed { get; set; }
        public bool Bes { get; set; }
        public double bedIN { get; set; }
        public double bedOut { get; set; }
        public double besIN { get; set; }
        public double besOut { get; set; }
    }
}
