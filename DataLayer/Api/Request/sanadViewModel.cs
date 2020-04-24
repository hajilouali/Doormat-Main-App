using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Api.Request
{
    public class sanadViewModel
    {
        public string ShamsiDate { get; set; }

        [Display(Name = "توضیحات")]
        public string Discription { get; set; }
        public List<SanadViewModel> Sanads { get; set; }
    }
    public class SanadViewModel
    {
        [Display(Name ="عنوان")]
        public int AccountingHeading_ID { get; set; }
        [Display(Name = "بدهکاری")]
        public int Bedehkari { get; set; }
        [Display(Name = "بستانکاری")]
        public int Bestankari { get; set; }
        [Display(Name = "شرح")]
        public string Comment { get; set; }
    }
}
