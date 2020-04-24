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

namespace WpfApp1.Pages.Manufacture
{
    /// <summary>
    /// Interaction logic for ManufacturyReport.xaml
    /// </summary>
    public partial class ManufacturyReport
    {
        private string _token;
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
        public ManufacturyReport(string token)
        {
            _token = token;
            InitializeComponent();
            InitializeRowContextMenuItems();
            EnableDate.IsChecked = true;
            this.txtendAt.Culture = new System.Globalization.CultureInfo("fa");
            this.txtendAt.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            this.txtendAt.SelectedDate = DateTime.Now;
            this.txtstartAt.Culture = new System.Globalization.CultureInfo("fa");
            this.txtstartAt.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            PersianCalendar pc = new PersianCalendar();
            this.txtstartAt.SelectedDate = new DateTime(pc.GetYear(DateTime.Now), pc.GetMonth(DateTime.Now), 01, pc);
            Grd();
        }
        private void Grd()
        {
            ManufactureRequest request = new ManufactureRequest() { AllTime=true,EndTime="",StartTime=""};
            if (EnableDate.IsChecked==true&& txtstartAt.SelectedDate!=null&& txtendAt.SelectedDate!=null)
            {
                request.AllTime = false;
                request.StartTime = txtstartAt.SelectedDate.Value.ToString("yyyy/MM/dd");
                request.EndTime = txtendAt.SelectedDate.Value.ToString("yyyy/MM/dd");
            }
            var rows = sdk.GetManufacturebytime(_token, request);
            if (rows.Result.IsSuccess)
            {
                grd.ItemsSource = rows.Result.Data;
            }
        }

        private void grd_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            if (e.Row.DataContext is ManufactureDto)
            {
                e.Row.Background = (e.Row.DataContext as ManufactureDto).ConditionManufacture == ConditionManufacture.PendingForConstruction ? new SolidColorBrush(Colors.White) : (e.Row.DataContext as ManufactureDto).ConditionManufacture == ConditionManufacture.Cut ? new SolidColorBrush(Colors.OrangeRed) : (e.Row.DataContext as ManufactureDto).ConditionManufacture == ConditionManufacture.Built ? new SolidColorBrush(Colors.Aqua) : new SolidColorBrush(Colors.LightGreen);
            }
        }
        private void InitializeRowContextMenuItems()
        {
            ObservableCollection<WpfApp1.Pages.Factor.MenuItem> items = new ObservableCollection<WpfApp1.Pages.Factor.MenuItem>();
            WpfApp1.Pages.Factor.MenuItem Item2 = new WpfApp1.Pages.Factor.MenuItem();
            Item2.Text = "نمایش فاکتور";
            items.Add(Item2);
            WpfApp1.Pages.Factor.MenuItem Item1 = new WpfApp1.Pages.Factor.MenuItem();
            Item1.Text = "نمایش پیوست ها";
            items.Add(Item1);
            WpfApp1.Pages.Factor.MenuItem Item3 = new WpfApp1.Pages.Factor.MenuItem();
            Item3.Text = "تغییر وضعیت";
            items.Add(Item3);
            this.rowContextMenuItems = items;
        }
        private void GridContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (this.ClickedHeader != null)
            {
                foreach (var item in this.rowContextMenuItems)
                {
                    item.IsEnabled = false;

                }
                this.GridContextMenu.ItemsSource = this.rowContextMenuItems;
            }
            else if (this.ClickedRow != null)
            {
                this.grd.SelectedItem = this.ClickedRow.DataContext;
                foreach (var item in this.rowContextMenuItems)
                {

                    item.IsEnabled = true;
                }
                this.GridContextMenu.ItemsSource = this.rowContextMenuItems;
            }
            else
            {
                foreach (var item in this.rowContextMenuItems)
                {

                    item.IsEnabled = false;




                }
                this.GridContextMenu.ItemsSource = this.rowContextMenuItems;
            }
        }
        private void GridContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {

            WpfApp1.Pages.Factor.MenuItem item = (e.OriginalSource as RadMenuItem).DataContext as WpfApp1.Pages.Factor.MenuItem;
            var Model = (grd.SelectedItem as ManufactureDto);

            switch (item.Text)
            {
                case "نمایش فاکتور":
                    {
                        var factorDto = sdk.GetFactorById(_token, Model.Factor_ID);
                        var client = sdk.GetClientById(_token, Model.FactorClientID);
                        FactorReports.FactorViewReport(factorDto.Result.Data, client.Result.Data);

                    }
                    break;
                case "نمایش پیوست ها":
                    {
                        WpfApp1.Pages.Factor.FactorAttachment factorAttachment = new WpfApp1.Pages.Factor.FactorAttachment(_token, Model.Factor_ID);
                        factorAttachment.Owner = this;
                        factorAttachment.Show();

                    }
                    break;
                case "تغییر وضعیت":
                    {

                        AddManufacuryHistory add = new AddManufacuryHistory(_token, Model.id);
                        add.Owner = this;
                        add.ShowDialog();
                    }
                    break;

            }

        }
        private void RadWindow_Activated(object sender, EventArgs e)
        {
            Grd();
        }

        private void EnableDate_Checked(object sender, RoutedEventArgs e)
        {
            Grd();
        }

        private void EnableDate_Unchecked(object sender, RoutedEventArgs e)
        {
            Grd();
        }

        private void txtstartAt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Grd();
        }
    }
}
