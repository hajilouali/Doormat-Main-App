using Newtonsoft.Json;
using System;
namespace PhoenixFutureApiSdk.Model
{

    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public ResponseStatus StatusCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
    public class ResponseModel<T> : ResponseModel
       
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
    }





   
}
