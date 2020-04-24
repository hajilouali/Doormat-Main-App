using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Api.Response
{
    public class ClientStatusDto
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public double Mandeh { get; set; }

        public string Vaziat { get; set; }
    }
}
