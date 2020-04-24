using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Telerik.Windows.Data;

namespace DataLayer.Api.Response
{
    public class ProductAndServiceDto
    {
        public ProductAndServiceDto()
        {
            UnitPrice = 0;
        }
        [DisplayName("ردیف")]
        public int id { get; set; }
        [DisplayName("نام محصول")]
        public string ProductName { get; set; }
        [DisplayName("کد محصول")]
        public string ProductCode { get; set; }
        [DisplayName("واحد")]
        public UnitType UnitType { get; set; }
        [DisplayName("قیمت واحد")]
        public int UnitPrice { get; set; }
        [DisplayName("نوع")]
        //public EnumDataSource ProductType
        //{
        //    get
        //    {
        //        return new EnumDataSource { EnumType = typeof(ProductType) };
        //    }

        //}
        public ProductType ProductType { get; set; }
    }
    public enum UnitType
    {
        [Display(Name = "مترمربع")]
        SquareMeters,
        [Display(Name = "متر")]
        Metr,
        [Display(Name = "عدد")]
        Unit,

    }
    public enum ProductType
    {
        [Display(Name = "محصول")]
        Product,
        [Display(Name = "سرویس")]
        Service
    }
}
