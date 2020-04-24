using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Api.Response
{
   public class FactorDto
    {
        [Display(Name = "شماره فاکتور")]
        public int id { get; set; }
        [Display(Name = "تاریخ")]
        public string DateTime { get; set; }
        [Display(Name = "قیمت کل")]
        public decimal TotalPrice { get; set; }
        [Display(Name = "ارزش افزوده")]
        public decimal Taxes { get; set; }
        [Display(Name = "تخفیف")]
        public decimal Discount { get; set; }
        [Display(Name = "کل")]
        public decimal FactorPrice { get; set; }
        [Display(Name = "کد کاربر")]
        public int User_ID { get; set; }
        [Display(Name = "کد طرف حساب")]
        public int Client_ID { get; set; }
        [Display(Name = "کد رهگیری فاکتور")]
        public string FactorCodeView { get; set; }
        [Display(Name = "نوع فاکتور")]
        public FactorType FactorType { get; set; }
        [Display(Name = "توضیحات")]
        public string Discription { get; set; }
        [Display(Name = "طرف حساب")]
        public string User_Name { get; set; }
        [Display(Name = "نام کاربر")]
        public string UserName { get; set; }
        public DateTime DateTimevalu { get; set; }
        public List<Product_FactorDto> Product_Factor { get; set; }
    }
    public class Product_FactorDto
    {

        public int id { get; set; }
        [Display(Name = "محصول")]
        public string ProductAndService { get; set; }
        public int ProductAndService_ID { get; set; }
        [Display(Name = "شماره فاکتور")]
        public int Factor_ID { get; set; }
        [Display(Name = "عرض")]
        public decimal Width { get; set; }
        [Display(Name = "طول")]
        public decimal length { get; set; }
        [Display(Name = "نعداد")]
        public int Unit { get; set; }
        [Display(Name = "قیمت")]
        public decimal Price { get; set; }
        [Display(Name = "متراژ")]
        public string Metraj { get; set; }
        [Display(Name = "شرح")]
        public string RowDiscription { get; set; }
        [Display(Name = "قیمت واحد")]
        public double UnitPrice { get; set; }

    }
    public enum FactorType
    {
        [Display(Name = "پیش فاکتور")]
        PishFactor,
        [Display(Name = " فاکتور")]
        Factor,
        [Display(Name = "در انتظار تایید")]
        PendingToAccept,
    }
}
