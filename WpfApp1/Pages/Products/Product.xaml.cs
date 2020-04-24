using DataLayer.Api.Response;
using DoormatSite.Tools;
using PhoenixFutureApiSdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Telerik.Windows.Data;

namespace WpfApp1.Pages.Products
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        public string token;
        public int ProductId=0;
        public Int32 selectedProdoctType=0;
        public Int32 selectedUnitType=0;
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public Product()
        {
            InitializeComponent();
            this.DataContext = new ProductsContext(null);
           


        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Title = txtTitle.Text;
            string Code = txtProductCode.Text;
            var Price = txtPrice.Value;
            var ProductType = cmbProductType.SelectedIndex;
            var UnitType = cmbUintType.SelectedIndex;
            if (ProductId!=0)
            {
                ProductAndServiceDto dto = new ProductAndServiceDto()
                {
                    id = ProductId,
                    ProductCode = Code,
                    ProductName = Title,
                    UnitPrice = (int)Price,
                    ProductType = (DataLayer.Api.Response.ProductType)ProductType,
                    UnitType = (DataLayer.Api.Response.UnitType)UnitType
                };
                var seult = sdk.EditeProduct(token, dto);

                if (seult.Result.IsSuccess)

                    this.Close();
                if (!seult.Result.IsSuccess)
                    MessageBox.Show(seult.Result.Message);
            }
            else
            {
                ProductAndServiceDto dto = new ProductAndServiceDto()
                {
                    id = ProductId,
                    ProductCode = Code,
                    ProductName = Title,
                    UnitPrice = (int)Price,
                    ProductType = (DataLayer.Api.Response.ProductType)ProductType,
                    UnitType = (DataLayer.Api.Response.UnitType)UnitType
                };
                var seult = sdk.AddProduct(token, dto);

                if (seult.Result.IsSuccess)

                    this.Close();
                if (!seult.Result.IsSuccess)
                    MessageBox.Show(seult.Result.Message);
            }
            

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ProductId != 0)
            {
                var product = sdk.GetProductByID(token, ProductId);
                if (product.Result.IsSuccess)
                {
                    selectedProdoctType = (int)product.Result.Data.ProductType;
                    selectedUnitType = (int)product.Result.Data.UnitType;
                    txtTitle.Text = product.Result.Data.ProductName;
                    txtProductCode.Text = product.Result.Data.ProductCode;
                    txtPrice.Value = product.Result.Data.UnitPrice;
                    this.Title = "ویرایش محصول";
                    
                }
                else
                {
                    MessageBox.Show("در دریافت اطلاعات محصول از سرور مشکلی پیش آمده ");
                }
                cmbProductType.SelectedIndex = selectedProdoctType;
                cmbUintType.SelectedIndex = selectedUnitType;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    
}
