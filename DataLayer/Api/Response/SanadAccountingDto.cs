using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Api.Response
{
    public class SanadAccountingDto
    {
        [Display(Name ="تاریخ فرمت")]
        public DateTime Datatime { get; set; }
        [Display(Name = "تاریخ ")]
        public string stringDatatime { get; set; }
        [Display(Name = "عنوان تیتر حساب")]
        public string AccountingHeading { get; set; }
        [Display(Name = "کد حساب")]
        public int AccountingHeading_ID { get; set; }
        [Display(Name = "شماره سند")]
        public int SanadHeading_ID { get; set; }
        [Display(Name = "بده کاری")]
        public int Bedehkari { get; set; }
        [Display(Name = "بستانکاری")]
        public int Bestankari { get; set; }
        [Display(Name = "مانده")]
        public int Mandeh { get; set; }
        [Display(Name = "تشخیص")]
        public string TashKhis { get; set; }
        [Display(Name = "شرح")]
        public string Comment { get; set; }
        public List<SanadAccountingDto> SanadAccountingDtoS { get; set; }
    }
}
