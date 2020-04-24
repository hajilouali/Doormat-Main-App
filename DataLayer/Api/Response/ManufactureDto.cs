using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Api.Response
{
   public class ManufactureDto
    {
        [Display(Name = "شماره تولید")]
        public int id { get; set; }
        [Display(Name = "شماره فاکتور")]
        public int Factor_ID { get; set; }
        [Display(Name = "شماره طرف حساب")]
        public int FactorClientID { get; set; }
        [Display(Name = "طرف حساب")]
        public string FactorClientName { get; set; }
        [Display(Name = "آدرس طرف حساب")]
        public string FactorClientaddress { get; set; }
        [Display(Name = "تلفن طرف حساب")]
        public string FactorClientPhone { get; set; }
        [Display(Name = "وضعیت مالی")]
        public ClientAccountingStatus ClientAccountingStatus { get; set; }
        [Display(Name = "تاریخ ورود به تولید")]
        public string InDateTime { get; set; }
        [Display(Name = "کد آخرین وضعیت")]
        public ConditionManufacture ConditionManufacture { get; set; }
        [Display(Name = "آخرین وضعیت")]
        public string ConditionManufactureTitle { get; set; }
        public List<ManufactureHistoryDto> ManufactureHistories { get; set; }                                   
        
    }

    public class ManufactureHistoryDto
    {
        [Display(Name = "کد کاربر")]
        public int User_ID { get; set; }
        [Display(Name = "نام کاربر")]
        public string UserFullname { get; set; }
        [Display(Name = "کد تولید")]
        public int Manufacture_ID { get; set; }
        [Display(Name = "زمان")]
        public string DateTime { get; set; }
        [Display(Name = "وضعیت")]
        public string ConditionManufactureTitle { get; set; }
        [Display(Name = "کد وضعیت")]
        public ConditionManufacture ConditionManufacture { get; set; }
        [Display(Name = "توضیحات")]
        public string Discription { get; set; }
    }

    public class ClientAccountingStatus
    {
        [Display(Name = "وضعیت مالی")]
        public string status { get; set; }
        [Display(Name = "مانده")]
        public double Price { get; set; }
    }

    public enum ConditionManufacture
    {
        [Display(Name = "در انتظار ساخت")]
        PendingForConstruction,
        [Display(Name = "برش خورده")]
        Cut,
        [Display(Name = "ساخته شده")]
        Built,
        [Display(Name = "تحویل به مشتری")]
        DeliverToClient,
        [Display(Name = "تحویل به همکار")]
        DeliverToPartner,
        [Display(Name = "نصب شده")]
        Install

    }
}
