using DataLayer.Api.Request;
using DataLayer.Api.Response;
using DoormatSite.Tools;
using PhoenixFutureApiSdk;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Telerik.Windows.Controls;
using WpfApp1.RepostFormTools;
using WpfApp1.RepostFormTools.ViewModel;

namespace WpfApp1.Reposts.Accounting
{
    /// <summary>
    /// Interaction logic for ClientAccounting.xaml
    /// </summary>
    public partial class ClientAccounting
    {
        private string _token;

        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public ClientAccounting(string token)
        {
            _token = token;
            InitializeComponent();
            grd.ItemsSource = sdk.GetAllClients(_token).Result.Data;

        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            ClientsAccpuntingReports dto = new ClientsAccpuntingReports();
            dto.ClientID = 0;
            dto.Bes = false;
            dto.Bed = false;
            dto.bedIN = 0;
            dto.bedOut = 0;
            dto.besIN = 0;
            dto.besOut = 0;
            if (chclient.IsChecked==true)
            {
                if (grd.SelectedItem!=null)
                {
                    dto.ClientID = (grd.SelectedItem as ClientDto).id;
                }
                else
                {
                    MessageBox.Show("لطفا طرف حساب را انتخاب نمایید", "", MessageBoxButton.OK);
                }
            }
            if (chbed.IsChecked==true)
            {
                if (!string.IsNullOrEmpty(txtbedin.Text)|| !string.IsNullOrEmpty(txtbedin.Text))
                {
                    dto.Bed = true;
                    dto.bedIN = Convert.ToDouble(txtbedin.Text);
                    dto.bedOut = Convert.ToDouble(txtbedout.Text);
                }
                else
                {
                    MessageBox.Show("لطفا مقدار بدهی را مشخص نمایید", "", MessageBoxButton.OK);
                }
            }
            if (ckbes.IsChecked == true)
            {
                if (!string.IsNullOrEmpty(txtbesin.Text) || !string.IsNullOrEmpty(txtbesout.Text))
                {
                    dto.Bes = true;
                    dto.besIN = Convert.ToDouble(txtbesin.Text);
                    dto.besOut = Convert.ToDouble(txtbesout.Text);
                }
                else
                {
                    MessageBox.Show("لطفا مقدار بستانکاری را مشخص نمایید", "", MessageBoxButton.OK);
                }
            }
            var rspons = sdk.ClientsAccountingReport(_token, dto);
            if (rspons.Result.IsSuccess)
            {
                PersianCalendar pc = new PersianCalendar();
                string dat = pc.GetYear(DateTime.Now).ToString() + "/" + pc.GetMonth(DateTime.Now).ToString() + "/" + pc.GetDayOfMonth(DateTime.Now).ToString();

                List<ClientReportsViewModel> model = new List<ClientReportsViewModel>();
                int row = 1;
                foreach (var item in rspons.Result.Data)
                {
                    ClientReportsViewModel dtod = new ClientReportsViewModel() {
                        ClientID = item.ClientID,
                        ClientName = item.ClientName,
                        tbSanadID = 0,
                        ReportDate = dat,
                        tbRow=row
                    };
                    switch (item.Vaziat)
                    {
                        case "بدهکار":
                            {
                                dtod.tbBedehkar = Convert.ToInt32(item.Mandeh);
                                break;
                            }
                        case "بستانکار":
                            {
                                dtod.tbBestankar = Convert.ToInt32(item.Mandeh);
                                break;
                            }

                        default:
                            break;
                    }
                    row++;
                    model.Add(dtod);
                }
                MoeinReport.MoeinAccountingCleints(model);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
