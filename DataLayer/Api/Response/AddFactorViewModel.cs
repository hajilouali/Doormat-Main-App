using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Api.Response
{
    public class AddFactorViewModel
    {
        public string ShamsiDate { get; set; }
        public int Client_ID { get; set; }
        public int User_ID { get; set; }
        public bool Rasmi { get; set; }
        public bool discountDefoult { get; set; }
        public int discoun { get; set; }
        public string Discription { get; set; }
        public List<Product_FactorViewModel> rows { get; set; }
    }
    public class Product_FactorViewModel
    {
        public Product_FactorViewModel()
        {
            UnitPrice = 0;
        }
        public int ProductAndService_ID { get; set; }
        public decimal Width { get; set; }
        public decimal length { get; set; }
        public int Unit { get; set; }
        public double UnitPrice { get; set; }

        public string RowDiscription { get; set; }
    }
}
