using DataLayer.Api.Response;
using DoormatSite.Tools;
using PhoenixFutureApiSdk;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using WpfApp1.RepostFormTools;

namespace WpfApp1.Pages.Manufacture
{
    /// <summary>
    /// Interaction logic for MainManufacture.xaml
    /// </summary>
    public partial class MainManufacture
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
        public MainManufacture(string token)
        {
            _token = token;
            InitializeComponent();
            InitializeRowContextMenuItems();
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 59), DispatcherPriority.Normal, delegate
            {
                Grd();
            }, this.Dispatcher);
            Grd();
        }
        private void Grd()
        {
            var rows = sdk.GetGetManufacture(_token);
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
            WpfApp1.Pages.Factor.MenuItem Item4 = new WpfApp1.Pages.Factor.MenuItem();
            Item4.Text = "صدور حواله خروج";
            items.Add(Item4);
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
                        var client = sdk.GetClientById(_token,Model.FactorClientID);
                        FactorReports.FactorViewReport(factorDto.Result.Data, client.Result.Data);

                    }
                    break;
                case "نمایش پیوست ها":
                    {
                        WpfApp1.Pages.Factor.FactorAttachment factorAttachment = new WpfApp1.Pages.Factor.FactorAttachment(_token,Model.Factor_ID);
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
                case "صدور حواله خروج":
                    {
                        var factorDto = sdk.GetFactorById(_token, Model.Factor_ID).Result.Data;
                        var client = sdk.GetClientById(_token, Model.FactorClientID).Result.Data;
                        FactorReports.HavalehViewReport(factorDto, client);
                    }
                    break;
            }

        }

        private void RadWindow_Activated(object sender, EventArgs e)
        {
            Grd();
        }
    }
}
