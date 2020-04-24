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

namespace WpfApp1.Pages.Bank
{
    /// <summary>
    /// Interaction logic for Bank.xaml
    /// </summary>
    public partial class Bank
    {
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        private string _token;
        public Bank(string token)
        {
            InitializeComponent();
            _token = token;
            DataBind();
        }
        public void DataBind()
        {
            var products = sdk.GetAllBanks(_token).Result;
            if (products.IsSuccess)
            {
                gt.ItemsSource = products.Data;
            }
        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            AddBank addBank = new AddBank();
            addBank.Owner = this;
            addBank.Header = "افزودن";
            addBank._token = _token;
            addBank.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addBank.ShowDialog();
        }

        private void btnedite_Click(object sender, RoutedEventArgs e)
        {
            if (gt.SelectedItems.Count>0)
            {
                var bandk = gt.SelectedItem as BankDto;
                AddBank bank = new AddBank();
                bank.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                bank.Header = "ویرایش";
                bank.Owner = this;
                bank.id = bandk.id;
                bank._token = _token;
                bank.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("لطفا یک ردیف را جهت ویرایش انتخاب نمایید");
            }
        }

        private void btnremove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RadWindow_Activated(object sender, EventArgs e)
        {
            DataBind();
        }
    }
}
