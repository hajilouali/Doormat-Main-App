using DataLayer.Api.Response;
using DoormatSite.Tools;
using PhoenixFutureApiSdk;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;

namespace WpfApp1.Pages.Products
{
    /// <summary>
    /// Interaction logic for ProductMain.xaml
    /// </summary>
    public partial class ProductMain : Window
    {
        private string _token;
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public ProductMain(string token)
        {
            _token = token;
            InitializeComponent();
            DataBind();
        }
        public void DataBind()
        {
            var products = sdk.GetAllProducts(_token).Result;
            this.DataContext = new ProductsContext(products.Data);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var products = sdk.GetAllProducts(token).Result;
            
            //if (products.IsSuccess)
            //{
            //    dg.ItemsSource = products.Data;

            //}

        }




        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (dg.SelectedCells.Count > 0)
            {
                var dataRow = dg.SelectedItem as ProductAndServiceDto;
                Product product = new Product();
                product.token = _token;
                product.ProductId = dataRow.id;
                product.Owner = this;
                product.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                product.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("لطفا یک محصول را جهت ویرایش انتخاب نمایید");
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.token = _token;
            product.Title = "افزودن محصول جدید";
            product.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            product.Owner = this;
            
            product.ShowDialog();
            
        }

        private void Window_GiveFeedback(object sender, System.Windows.GiveFeedbackEventArgs e)
        {
            
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            DataBind();
        }

        private void Window_ContentRendered(object sender, System.EventArgs e)
        {
            
        }

        private void Window_Initialized(object sender, System.EventArgs e)
        {
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }

        private void Window_PreviewGiveFeedback(object sender, System.Windows.GiveFeedbackEventArgs e)
        {
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedCells.Count > 0)
            {
                var dataRow = dg.SelectedItem as ProductAndServiceDto;
                var s = System.Windows.Forms.MessageBox.Show("آیا از حذف کالای انتخاب شده اطمینان دارید؟", "اخطار", MessageBoxButtons.OKCancel);
                if ( s==System.Windows.Forms.DialogResult.OK )
                {
                    var result = sdk.RemoveProduct(_token, dataRow.id);
                    if (result.Result.IsSuccess)
                    {
                        DataBind();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("در فرایند حذف محصول از سرور مشکلی پیش آمده");
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("لطفا یک محصول را جهت حذف انتخاب نمایید");
            }
        }
    }
}
