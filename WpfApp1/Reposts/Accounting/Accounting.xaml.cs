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

namespace WpfApp1.Reposts.Accounting
{
    /// <summary>
    /// Interaction logic for Accounting.xaml
    /// </summary>
    public partial class Accounting
    {
        private string _token;
        
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public Accounting(string token)
        {
            InitializeComponent();
            _token = token;
            
            EnableDate.IsChecked = true;
            this.txtendAt.Culture = new System.Globalization.CultureInfo("fa");
            this.txtendAt.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            this.txtendAt.SelectedDate = DateTime.Now;
            this.txtstartAt.Culture = new System.Globalization.CultureInfo("fa");
            this.txtstartAt.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            PersianCalendar pc = new PersianCalendar();
            this.txtstartAt.SelectedDate = new DateTime(pc.GetYear(DateTime.Now), pc.GetMonth(DateTime.Now), 01, pc);
            

        }
        private void gridItemSurce()
        {
            if (this.IsActiveWindow)
            {
                var dto = new AcountingReviw();
                dto.StartDate = "";
                dto.EndDate = "";
                if (EnableDate.IsChecked == true)
                {
                    dto.StartDate = txtstartAt.SelectedValue.Value.ToString("yyyy/MM/dd");
                    dto.EndDate = txtendAt.SelectedValue.Value.ToString("yyyy/MM/dd");
                }
                var respons = sdk.GetAccounting(_token, dto);
                if (respons.Result.IsSuccess)
                {
                    grd.ItemsSource = respons.Result.Data;
                    double bedehkar = 0;
                    double mandeh = 0;
                    double bestankat = 0;
                    foreach (var item in respons.Result.Data)
                    {
                        bedehkar += Math.Abs(item.Bedehkari);
                        bestankat += Math.Abs(item.Bestankari);
                        mandeh = bestankat - bedehkar;
                    }
                    txtmandeh.Text = mandeh.ToString("n0");

                    txtbedehkar.Text = bedehkar.ToString("n0");
                    txtbestankat.Text = bestankat.ToString("n0");
                }
               
            }
        }
       
        private void RadWindow_Activated(object sender, EventArgs e)
        {
            gridItemSurce();
        }

        private void ReportGrid_Click(object sender, RoutedEventArgs e)
        {
            var dto = new List<SanadAccountingDto>();
            foreach (var item in grd.Items)
            {
                dto.Add(item as SanadAccountingDto);

            }
            List<ClientReportsViewModel> list = new List<ClientReportsViewModel>();
            int row = 1;
            PersianCalendar pc = new PersianCalendar();
            string dat = pc.GetYear(DateTime.Now).ToString() + "/" + pc.GetMonth(DateTime.Now).ToString() + "/" + pc.GetDayOfMonth(DateTime.Now).ToString();

            foreach (var item in dto)
            {
                var dd = new ClientReportsViewModel() {
                ClientID=1,
                ClientName="مرور حساب",
                tbRow=row,
                tbBedehkar=item.Bedehkari,
                tbBestankar=item.Bestankari,
                tbDiscription=item.Comment,
                tbDate=item.Datatime.ToString(),
                tbSanadID=0,
                ReportDate= dat
                };
                list.Add(dd);
                row++;
            }
            MoeinReport.MoeinDaftar(list);
        }

        private void EnableDate_Checked(object sender, RoutedEventArgs e)
        {
            gridItemSurce();
        }

        private void EnableDate_Unchecked(object sender, RoutedEventArgs e)
        {
            gridItemSurce();
        }

        private void grd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = grd.SelectedItem as SanadAccountingDto;
            ChlidAccounting chlid = new ChlidAccounting(item);
            chlid.Owner = this;
            chlid.ShowDialog();
        }
    }
}
