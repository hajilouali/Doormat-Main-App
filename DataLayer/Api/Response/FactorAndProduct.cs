using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Api.Response
{
    public class FactorAndProduct
    {
        public FactorAndProduct()
        {
            Metraj = 0;
        }
        [Display(Name ="شماره")]
        public int id { get; set; }
        [Display(Name = "محصول")]
        public int ProductAndService_ID { get; set; }
        [Display(Name = "شماره فاکتور")]
        public int Factor_ID { get; set; }
        [Display(Name = "عرض")]
        public decimal Width { get; set; }
        [Display(Name = "طول")]
        public decimal length { get; set; }
        [Display(Name = "تعداد")]
        public int Unit { get; set; }
        [Display(Name = "متراژ")]
        public decimal Metraj { get; set; }
        [Display(Name = "قیمت")]
        public decimal Price { get; set; }
        [Display(Name = "شرح")]
        public string rowDiscription { get; set; }
    }
    
}
