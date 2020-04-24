using DataLayer.Api.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.model
{
   public class UsersByRoll
    {
        [Display(Name = "کد کاربر")]
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
        public IList<Roll> Rolls { get; set; }
    }
    
}
