using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Api.Response
{
    public class TiketDto
    {
        [Display(Name ="شماره تیکت")]
        public int id { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        public bool Closed { get; set; }
        [Display(Name = "سطح")]
        public short Level { get; set; }
        [Display(Name = "سطح")]
        public string LevelString { get; set; }
        [Display(Name = "دپارتمان")]
        public short Department { get; set; }
        [Display(Name = "دپارتمان")]
        public string DepartmentString { get; set; }
        public bool IsAdminSide { get; set; }
        [Display(Name = "کد کاربر")]
        public int UserID { get; set; }
        [Display(Name = "کاربر")]
        public string user { get; set; }
        [Display(Name = "تاریخ")]
        public DateTime DataCreate { get; set; }
        [Display(Name = "تاریخ")]
        public string DataCreateString { get; set; }
        [Display(Name = "تاریخ بروز رسانی")]

        public DateTime DataModified { get; set; }
        [Display(Name = "تاریخ بروز رسانی")]
        public string DataModifiedstring { get; set; }
        public List<TiketContentDto> tiketContents { get; set; }
    }
    public class TiketContentDto 
    {
        public int id { get; set; }
        public int tiketId { get; set; }
        public bool isAdminSide { get; set; }
        public string text { get; set; }
        public string fileURL { get; set; }
        public int userID { get; set; }
        public string user { get; set; }
        public DateTime dataCreate { get; set; }
        public DateTime dataModified { get; set; }
    }
}
