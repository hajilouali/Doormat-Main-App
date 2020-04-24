using DataLayer.Api.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer
{
    public class RowFactorViewModel
    {
        [Display(Name ="ردیف")]
        public int id { get; set; }
        [Display(Name = "کد محصول")]
        public int Productid { get; set; }
        [Display(Name = "نام")]
        public string ProductName { get; set; }
        [Display(Name = "تعداد")]
        public int Cunt { get; set; }
        [Display(Name = "عرض")]
        public double Withe { get; set; }
        [Display(Name = "طول")]
        public double Hight { get; set; }
        [Display(Name = "واحد")]
        public UnitType UnitType { get; set; }
        [Display(Name = "قیمت ")]
        public double Price { get; set; }
        public ProductAndServiceDto ProductAndServiceDto { get; set; }

    }
}
