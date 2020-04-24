using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Api.Response
{
   public class UserModel
    {
        [Display(Name ="کد کاربر")]
        public int id { get; set; }
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Display(Name = "کاربر")]
        public string FullName { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "فعال؟")]
        public bool IsActive { get; set; }
        [Display(Name = "نقش ها")]
        public IList<string> RollName { get; set; }
        public decimal discount { get; set; }
        public string userAddress { get; set; }
        public string userPhone { get; set; }
    }
}
