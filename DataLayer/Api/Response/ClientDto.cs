using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Api.Response
{
   public class ClientDto
    {
        [Display(Name = "کد طرف حساب")]
        public int id { get; set; }
        
        public int User_ID { get; set; }
        [Display(Name = "نام طرف حساب")]
        public string ClientName { get; set; }
        [Display(Name = "آدرس")]
        public string ClientAddress { get; set; }
        [Display(Name = "شماره تماس")]
        public string ClientPhone { get; set; }
        [Display(Name = "شناسه ملی")]
        public string CodeMeli { get; set; }
        [Display(Name = "کد اقتصادی")]
        public string CodeEgtesadi { get; set; }
        [Display(Name = "درصد تخفیف")]
        public decimal DiscountPercent { get; set; }
        [Display(Name = "حد اعتبار")]
        public double MaxCreditValue { get; set; }
        [Display(Name = "نام رابط")]
        public string ClientPartnerName { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", this.ClientPhone);
        }
    }
}
