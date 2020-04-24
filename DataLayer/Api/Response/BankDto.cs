using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Api.Response
{
    public class BankDto
    {
        [Display(Name ="شماره بانک")]
        public int id { get; set; }
        [Display(Name = "عنوان بانک")]
        public string BankTitle { get; set; }
        
        public int AccountingHeading_ID { get; set; }
    }
}
