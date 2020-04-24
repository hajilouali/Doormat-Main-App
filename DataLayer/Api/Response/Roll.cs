using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Api.Response
{
    public class Roll
    {
        [Display(Name ="شماره")]
        public int id { get; set; }
        [Display(Name = "نام نقش")]
        public string name { get; set; }
        [Display(Name = "شرح")]
        public string description { get; set; }
        
    }
}
