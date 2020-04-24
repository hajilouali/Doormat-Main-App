using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Api.Response
{
   public class User
    {
        public int id { get; set; }
        public string FullName { get; set; }

        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
    }
}
