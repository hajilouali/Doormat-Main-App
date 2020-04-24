using DataLayer.Api.Response;
using DoormatSite.Tools;
using PhoenixFutureApiSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Telerik.Windows.Controls;

namespace WpfApp1.Pages.Factor
{
    /// <summary>
    /// Interaction logic for FindProduct.xaml
    /// </summary>
    public partial class FindProduct
    {
        public int ProductID { get; set; }
        private string _token;
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public FindProduct(string token)
        {
            InitializeComponent();
            _token = token;
            this.DataContext = this;
            grdsearch.ItemsSource = sdk.GetAllProducts(_token).Result.Data;
        }

        private void grdsearch_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var p = grdsearch.SelectedItem as ProductAndServiceDto;
            ProductID = p.id;
            this.DialogResult = true;
            this.Close();
        }

        private void RadWindow_Closed(object sender, WindowClosedEventArgs e)
        {
           
        }

        private void grdsearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                var p = grdsearch.SelectedItem as ProductAndServiceDto;
                ProductID = p.id;
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
