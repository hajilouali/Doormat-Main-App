using DataLayer.Api.Response;
using DoormatSite.Tools;
using PhoenixFutureApiSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1.Pages.Clients
{
    /// <summary>
    /// Interaction logic for Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        private string _token;
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public Clients(string token)
        {
            _token = token;
            InitializeComponent();
            DataBind();
        }
        public void DataBind()
        {
            var products = sdk.GetAllClients(_token).Result;
            this.DataContext = new ClientsContext(products.Data);
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient();
            addClient.token = _token;
            addClient.Owner = this;
            addClient.Header = "افزودن طرف حساب";
            addClient.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            DataBind();
        }

        private void btnEditeClient_Click(object sender, RoutedEventArgs e)
        {
            var dataRow = grdClients.SelectedItem as ClientDto;
            if (grdClients.SelectedItems.Count>0)
            {
                AddClient addClient = new AddClient();
                addClient.token = _token;
                addClient.id = dataRow.id;
                addClient.Owner = this;
                addClient.Header = "ویرایش طرف حساب";
                addClient.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show("لطفا یک طرف حساب را جهت ویرایش انتخاب نمایید");
            }
        }

        private void btnRemoveClient_Click(object sender, RoutedEventArgs e)
        {
            if (grdClients.SelectedCells.Count > 0)
            {
                var dataRow = grdClients.SelectedItem as ClientDto;
                var s = System.Windows.Forms.MessageBox.Show("آیا از حذف طرف حساب انتخاب شده اطمینان دارید؟", "اخطار", MessageBoxButtons.OKCancel);
                if (s == System.Windows.Forms.DialogResult.OK)
                {
                    var result = sdk.RemoveClient(_token, dataRow.id);
                    if (result.Result.IsSuccess)
                    {
                        DataBind();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("در فرایند حذف طرف حساب از سرور مشکلی پیش آمده");
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("لطفا یک طرف حساب را جهت حذف انتخاب نمایید");
            }
        }
    }
}
