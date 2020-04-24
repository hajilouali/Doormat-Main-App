using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DataLayer.Api;
using Newtonsoft.Json;

namespace DataLayer
{
    public  class DataApi
    {
        public  async Task<DateRespuns> GetDateOccasion()
        {
            PersianCalendar pc = new PersianCalendar();
            string urlget = string.Format("https://farsicalendar.com/api/sh/" + pc.GetDayOfMonth(DateTime.Now) + "/" + pc.GetMonth(DateTime.Now));

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var response =  client.GetAsync(urlget).Result;
                    if (response != null)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<DateRespuns>(jsonString);
                    }
                }
            }
            catch 
            {
                var respons = new DateRespuns()
                {
                    type = typeDateRespuns.failed,
                    values = null
                };
                return respons;
            }
            var respon = new DateRespuns()
            {
                type = typeDateRespuns.failed,
                values = null
            };
            return respon;

        }
        
    }
}