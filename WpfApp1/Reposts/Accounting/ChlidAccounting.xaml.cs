using DataLayer.Api.Request;
using DataLayer.Api.Response;
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
    /// Interaction logic for ChlidAccounting.xaml
    /// </summary>
    public partial class ChlidAccounting
    {
        
        private SanadAccountingDto _Lists { get; set; }
        public ChlidAccounting(SanadAccountingDto Lists)
        {
            _Lists = Lists;
            InitializeComponent();
        }
        private void gridItemSurce()
        {
            if (this.IsActiveWindow)
            {
                var dto = new AcountingReviw();
                var respons = _Lists;
               
                
                if (respons!=null)
                {
                    grd.ItemsSource = respons.SanadAccountingDtoS;
                    double bedehkar = 0;
                    double mandeh = 0;
                    double bestankat = 0;
                    foreach (var item in respons.SanadAccountingDtoS)
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
                var dd = new ClientReportsViewModel()
                {
                    ClientID = 1,
                    ClientName = _Lists.AccountingHeading,
                    tbRow = row,
                    tbBedehkar = item.Bedehkari,
                    tbBestankar = item.Bestankari,
                    tbDiscription = item.Comment,
                    tbDate = item.stringDatatime,
                    tbSanadID = item.SanadHeading_ID,
                    ReportDate = dat
                };
                list.Add(dd);
                row++;
            }
            MoeinReport.Moein(list);
        }

        
    }
}
