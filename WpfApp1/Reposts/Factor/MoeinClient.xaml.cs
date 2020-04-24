using DataLayer.Api.Request;
using DataLayer.Api.Response;
using DoormatSite.Tools;
using PhoenixFutureApiSdk;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using WpfApp1.RepostFormTools;
using WpfApp1.RepostFormTools.ViewModel;

namespace WpfApp1.Reposts.Factor
{
    /// <summary>
    /// Interaction logic for MoeinClient.xaml
    /// </summary>
    public partial class MoeinClient
    {
        private int ClientID;
        private string StartDate;
        private string endDate;
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        private string _token;
        public MoeinClient(string token)
        {
            InitializeComponent();
            _token = token;
            grdclients.ItemsSource = sdk.GetAllClients(token).Result.Data;
            this.txtdate.Culture = new System.Globalization.CultureInfo("fa");
            this.txtdate.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            this.txtdate.SelectedDate = DateTime.Now;
            this.txtenddate.Culture = new System.Globalization.CultureInfo("fa");
            this.txtenddate.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            this.txtenddate.SelectedDate = DateTime.Now;
            this.txtstartdate.Culture = new System.Globalization.CultureInfo("fa");
            this.txtstartdate.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            this.txtstartdate.SelectedDate = DateTime.Now;
            
        }

        private void rdall_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void rdindate_Checked(object sender, RoutedEventArgs e)
        {
           
        }

        private void rdbetwendate_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            busycontent.IsBusy = true;
            if (grdclients.SelectedItem!=null)
            {
                var client = grdclients.SelectedItem as ClientDto;
                List<SanadDto> respons = null;
                ClientMoein dto = new ClientMoein();
                dto.ClientID = client.id;
                if (rdall.IsChecked == true)
                {
                    dto.EndDate = "";
                    dto.StartDate = "";
                }
                if (rdindate.IsChecked == true)
                {
                    dto.EndDate = txtdate.SelectedDate.Value.ToString("yyyy/MM/dd");
                    dto.StartDate = txtdate.SelectedDate.Value.ToString("yyyy/MM/dd");
                }
                if (rdbetwendate.IsChecked == true)
                {
                    dto.EndDate = txtenddate.SelectedDate.Value.ToString("yyyy/MM/dd");
                    dto.StartDate = txtstartdate.SelectedDate.Value.ToString("yyyy/MM/dd");
                }
                respons = sdk.GetClientMoein(_token, dto).Result.Data;
                List<ClientReportsViewModel> model = new List<ClientReportsViewModel>();
                PersianCalendar pc = new PersianCalendar();
                string dat= pc.GetYear(DateTime.Now).ToString()+"/"+ pc.GetMonth(DateTime.Now).ToString() + "/"+pc.GetDayOfMonth(DateTime.Now).ToString();
                int row = 1;
                if (respons!=null)
                {
                    foreach (var item in respons)
                    {
                        model.Add(new ClientReportsViewModel()
                        {
                            ClientAddress = client.ClientAddress,
                            ClientID = client.id,
                            ClientName = client.ClientName,
                            ClientPhone = client.ClientPhone,
                            ReportDate = dat,
                            tbBedehkar = item.Bedehkari,
                            tbBestankar = item.Bestankari,
                            tbDate = item.stringDatatime,
                            tbDiscription = item.Comment,
                            tbSanadID = item.SanadHeading_ID,
                            tbRow = row

                        });
                        row++;
                    }
                }
                


                MoeinReport.ClientMoein(model);
                
                this.Close();
            }
            else
            {
                MessageBox.Show("لطفا طرف حساب  را انتخاب نمایید");
            }
            busycontent.IsBusy = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
