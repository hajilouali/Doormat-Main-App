using DataLayer.Api.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Data;

namespace WpfApp1.Pages.Products
{
    public class ProductViewModel
    {
        [DisplayName("ردیف")]
        public int id { get; set; }
        [DisplayName("نام محصول")]
        public string ProductName { get; set; }
        [DisplayName("کد محصول")]
        public string ProductCode { get; set; }
        [DisplayName("واحد")]
        public UnitType UnitType { get; set; }
        [DisplayName("قیمت واحد")]
        public int UnitPrice { get; set; }
        [DisplayName("نوع")]

        public EnumDataSource ProductType
        {
            get
            {
                return new EnumDataSource { EnumType = typeof(ProductType) };
            }
            set
            {

            }
        }

        public ProductViewModel()
        {

        }
    }
}
