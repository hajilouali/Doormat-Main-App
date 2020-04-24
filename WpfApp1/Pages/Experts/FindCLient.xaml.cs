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

namespace WpfApp1.Pages.Experts
{
    /// <summary>
    /// Interaction logic for FindCLient.xaml
    /// </summary>
    public partial class FindCLient
    {
        public string _token { get; set; }
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public int ClientID = 0;
        public FindCLient(string token)
        {
            _token = token;
            InitializeComponent();
            var clients = sdk.GetAllClients(token);
            if (clients.Result.IsSuccess)
            {
                grd.ItemsSource = clients.Result.Data;
            }
        }

        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            if (grd.SelectedItem != null)
            {
                ClientID = (grd.SelectedItem as ClientDto).id;
                this.Close();
            }
        }

        private void grd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grd.SelectedItem != null)
            {
                ClientID = (grd.SelectedItem as ClientDto).id;
                this.Close();
            }
        }

       
    }
}
