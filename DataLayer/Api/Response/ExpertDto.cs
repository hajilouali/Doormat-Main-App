using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Api.Response
{
    public class ExpertDto
    {
        [Display(Name ="شماره بازدید")]
        public int id { get; set; }
        [Display(Name = "تاریخ")]
        public string DateTime { get; set; }
        [Display(Name = "کد وضعیت")]
        public ExpertCordition ExpertCordition { get; set; }
        [Display(Name = "وضعیت")]
        public string ExpertCorditionTitle { get; set; }
        [Display(Name = "کد مشتری")]
        public int Client_ID { get; set; }
        [Display(Name = "نام مشتری")]
        public string ClientName { get; set; }
        [Display(Name = "آدرس مشتری")]
        public string Clientaddress { get; set; }
        [Display(Name = "تلفن مشتری")]
        public string Clientphone { get; set; }
        [Display(Name = "تاریخچه")]
        public List<ExpertHistoryDto> ExpertHistores { get; set; }
    }
    public class ExpertHistoryDto
    {
        [Display(Name = "کد وضعیت")]
        public ExpertCordition ExpertCordition { get; set; }
        [Display(Name = "وضعیت")]
        public string ExpertCorditionTitle { get; set; }
        [Display(Name = "تاریخ")]
        public string DateTime { get; set; }
        [Display(Name = "کد کاربر")]
        public int User_ID { get; set; }
        [Display(Name = "کاربر")]
        public string UserName { get; set; }
        [Display(Name = "شماره بازدید")]
        public int Expert_ID { get; set; }
        [Display(Name = "شماره فاکتور")]
        public int Facor_ID { get; set; }
        [Display(Name = "شماره ")]
        public int id { get; set; }
        [Display(Name = "آخرین وضعیت فاکتور ")]
        public string LastFactorStatus { get; set; }
    }
    public enum ExpertCordition
    {
        [Display(Name = "در انتظار بازدید")]
        PendingForVisit,
        [Display(Name = "بازدید شده")]
        Issued,

    }
}
