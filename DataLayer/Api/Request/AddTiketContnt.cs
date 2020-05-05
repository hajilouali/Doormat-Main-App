using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Api.Request
{
   public class AddTiketContnt
    {
        [StringLength(1000)]
        public string Text { get; set; }

        public IFormFile File { get; set; }
    }
}
