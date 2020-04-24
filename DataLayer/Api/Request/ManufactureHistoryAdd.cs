using DataLayer.Api.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Api.Request
{
    public class ManufactureHistoryAdd
    {
        public int Manufactury_ID { get; set; }
        public string Discription { get; set; }
        public ConditionManufacture ConditionManufacture { get; set; }
    }
}
