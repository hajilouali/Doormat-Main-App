using DataLayer.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Data;

namespace WpfApp1.Pages.Products
{
    public class ProductAddViewModel
    {
        public ProductType SelectedProductType { get; set; }
        public ProductAddViewModel()
        {
            SelectedProductType = ProductType.Product;
        }
        public EnumDataSource ProductTypeSource
        {
            get
            {
                return new EnumDataSource { EnumType = typeof(ProductType) };
            }
        }
    }
}
