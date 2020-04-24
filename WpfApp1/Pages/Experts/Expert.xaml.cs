using DataLayer.Api.Request;
using DataLayer.Api.Response;
using DoormatSite.Tools;
using PhoenixFutureApiSdk;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Telerik.Windows.Controls.GridView;
using WpfApp1.RepostFormTools;

namespace WpfApp1.Pages.Experts
{
    /// <summary>
    /// Interaction logic for Expert.xaml
    /// </summary>
    public partial class Expert
    {
        public string _token { get; set; }
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        private ObservableCollection<WpfApp1.Pages.Factor.MenuItem> rowContextMenuItems;
        private GridViewHeaderCell ClickedHeader
        {
            get
            {
                return this.GridContextMenu.GetClickedElement<GridViewHeaderCell>();
            }
        }
        private GridViewRow ClickedRow
        {
            get
            {
                return this.GridContextMenu.GetClickedElement<GridViewRow>();
            }
        }
        public Expert(string token)
        {
            _token = token;
            InitializeComponent();
            EnableDate.IsChecked = true;
            this.txtendAt.Culture = new System.Globalization.CultureInfo("fa");
            this.txtendAt.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            this.txtendAt.SelectedDate = DateTime.Now;
            this.txtstartAt.Culture = new System.Globalization.CultureInfo("fa");
            this.txtstartAt.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            PersianCalendar pc = new PersianCalendar();
            this.txtstartAt.SelectedDate = new DateTime(pc.GetYear(DateTime.Now), pc.GetMonth(DateTime.Now), 01, pc);
            InitializeRowContextMenuItems();
            grdcalulat();
        }
        private void grdcalulat()
        {
            ManufactureRequest request = new ManufactureRequest() {
                AllTime = true,
                EndTime = "",
                StartTime=""
            
            };
            if (EnableDate.IsChecked==true&& txtstartAt.SelectedValue!=null&& txtendAt.SelectedValue!=null)
            {
                request.AllTime = false;
                request.StartTime = txtstartAt.SelectedValue.Value.ToString("yyyy/MM/dd");
                request.EndTime = txtendAt.SelectedValue.Value.ToString("yyyy/MM/dd");
            }
            var respons = sdk.GetExpertsbytime(_token, request);
            if (respons.Result.IsSuccess)
            {
                grd.ItemsSource = respons.Result.Data;
            }

        }
        private void InitializeRowContextMenuItems()
        {
            ObservableCollection<WpfApp1.Pages.Factor.MenuItem> items = new ObservableCollection<WpfApp1.Pages.Factor.MenuItem>();
                       WpfApp1.Pages.Factor.MenuItem Item1 = new WpfApp1.Pages.Factor.MenuItem();
            Item1.Text = "نمایش فاکتور";
            items.Add(Item1);
            WpfApp1.Pages.Factor.MenuItem Item2 = new WpfApp1.Pages.Factor.MenuItem();
            Item2.Text = "پیوست های فاکتور";
            items.Add(Item2);
            this.rowContextMenuItems = items;
        }
        private void GridContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (this.ClickedHeader != null)
            {
                foreach (var item in this.rowContextMenuItems)
                {
                    if (!item.Text.Equals("ایجاد برنامه بازدید"))
                    {
                        item.IsEnabled = false;
                    }

                }
                this.GridContextMenu.ItemsSource = this.rowContextMenuItems;
            }
            else if (this.ClickedRow != null)
            {
                this.grd.SelectedItem = this.ClickedRow.DataContext;
                foreach (var item in this.rowContextMenuItems)
                {

                    item.IsEnabled = true;
                    if (item.Text.Equals("نمایش فاکتور"))
                    {
                        try
                        {
                            var ob = grd.CurrentCellInfo.Item;
                            if (ob == null)
                            {
                                item.IsEnabled = false;
                            }
                        }
                        catch (Exception)
                        {

                            item.IsEnabled = false;
                        }

                    }
                }
                this.GridContextMenu.ItemsSource = this.rowContextMenuItems;
            }
            else
            {
                foreach (var item in this.rowContextMenuItems)
                {
                    if (!item.Text.Equals("ایجاد برنامه بازدید"))
                    {
                        item.IsEnabled = false;
                    }



                }
                this.GridContextMenu.ItemsSource = this.rowContextMenuItems;
            }
        }
        private void GridContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {

            WpfApp1.Pages.Factor.MenuItem item = (e.OriginalSource as RadMenuItem).DataContext as WpfApp1.Pages.Factor.MenuItem;
            switch (item.Text)
            {
               
                case "نمایش فاکتور":
                    {
                        var expertid = (grd.SelectedItem as ExpertDto);
                        if (expertid!=null)
                        {
                            var factor = sdk.GetFactorById(_token, expertid.ExpertHistores.SingleOrDefault().Facor_ID);
                            if (factor.Result.IsSuccess)
                            {
                                FactorReports.FactorViewReport(factor.Result.Data, sdk.GetClientById(_token, factor.Result.Data.Client_ID).Result.Data);
                            }
                        }
                        
                    }
                    break;
                case "پیوست های فاکتور":
                    {
                        var expertid = (grd.SelectedItem as ExpertDto);
                        if (expertid != null)
                        {
                            WpfApp1.Pages.Factor.FactorAttachment attachment = new Factor.FactorAttachment(_token, expertid.id);
                            attachment.Owner = this;
                            attachment.Show();
                        }

                    }
                    break;

            }
        }
        private void RadWindow_Activated(object sender, EventArgs e)
        {
            grdcalulat();
        }

        private void EnableDate_Checked(object sender, RoutedEventArgs e)
        {
            grdcalulat();
        }

        private void txtstartAt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grdcalulat();
        }
        
    }
}
