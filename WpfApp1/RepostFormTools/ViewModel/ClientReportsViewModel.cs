using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.RepostFormTools.ViewModel
{
    public class ClientReportsViewModel
    {
        public string ClientName { get; set; }
        public int ClientID { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPhone { get; set; }
        public int tbRow { get; set; }
        public int tbSanadID { get; set; }
        public string tbDate { get; set; }
        public string tbDiscription { get; set; }
        public int tbBedehkar { get; set; }
        public int tbBestankar { get; set; }
        public string ReportDate { get; set; }

    }
}
