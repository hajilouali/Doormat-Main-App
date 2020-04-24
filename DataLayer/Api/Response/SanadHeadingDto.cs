using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Api.Response
{
    public class SanadHeadingDto
    {
        public int id { get; set; }
        public int FactorID { get; set; }
        public string StringDateTime { get; set; }
        public string Discription { get; set; }
        public List<SanadDto> Sanads { get; set; }
    }
    public class SanadDto
    {
        public string stringDatatime { get; set; }
        public string AccountingHeading { get; set; }
        public int AccountingHeading_ID { get; set; }
        public int SanadHeading_ID { get; set; }
        public int Bedehkari { get; set; }
        public int Bestankari { get; set; }
        public string Comment { get; set; }
    }
}
