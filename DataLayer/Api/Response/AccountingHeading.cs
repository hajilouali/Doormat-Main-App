using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Api.Response
{
    public enum AccountingType
    {
        [Display(Name = "بدهکار")]
        debt,
        [Display(Name = "بستانکار")]
        Crediting
    }
    public class AccountingHeading 
    {
        [Display(Name = "کد سرفصل")]
        public int id { get; set; }
        [Display(Name = "سرفصل")]
        public string Title { get; set; }
        [Display(Name = "نوع")]
        public AccountingType AccountingType { get; set; }
        [Display(Name = "توضیحات")]
        public string Discription { get; set; }

       
    }
}
