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

using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace WpfApp1.Pages.Experts
{
    /// <summary>
    /// Interaction logic for ExpertsMain.xaml
    /// </summary>
    public partial class ExpertsMain
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
        public ExpertsMain(string token)
        {
            _token = token;
            InitializeComponent();
            InitializeRowContextMenuItems();
            grdcalulat();
        }
        private void grdcalulat()
        {
            var respons = sdk.GetExperts(_token);
            if (respons.Result.IsSuccess)
            {
                grd.ItemsSource = respons.Result.Data;
            }

        }
        private void InitializeRowContextMenuItems()
        {
            ObservableCollection<WpfApp1.Pages.Factor.MenuItem> items = new ObservableCollection<WpfApp1.Pages.Factor.MenuItem>();
            WpfApp1.Pages.Factor.MenuItem Item2 = new WpfApp1.Pages.Factor.MenuItem();
            Item2.Text = "ایجاد برنامه بازدید";
            items.Add(Item2);
            WpfApp1.Pages.Factor.MenuItem Item4 = new WpfApp1.Pages.Factor.MenuItem();
            Item4.Text = "ویرایش";
            items.Add(Item4);
            WpfApp1.Pages.Factor.MenuItem addItem = new WpfApp1.Pages.Factor.MenuItem();
            addItem.Text = "حذف";
            items.Add(addItem);
            WpfApp1.Pages.Factor.MenuItem Item1 = new WpfApp1.Pages.Factor.MenuItem();
            Item1.Text = "صدور پیش فاکتور";
            items.Add(Item1);
           
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
                    if (item.Text.Equals("ویرایش")&& item.Text.Equals("حذف") && item.Text.Equals("صدور پیش فاکتور"))
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
                case "ایجاد برنامه بازدید":
                    {
                        FindCLient findCLient = new FindCLient(_token);
                        findCLient.Owner = this;
                        findCLient.ShowDialog();
                        var clientid = findCLient.ClientID;
                        if (clientid!=0)
                        {
                            var respons = sdk.addExpertsHistury(_token, clientid);
                            grdcalulat();
                        }
                        
                    }
                    break;
                case "ویرایش":
                    {
                        var expertid = (grd.SelectedItem as ExpertDto);
                        FindCLient findCLient = new FindCLient(_token);
                        findCLient.Owner = this;
                        findCLient.ShowDialog();
                        var clientid = findCLient.ClientID;
                        if (clientid != 0)
                        {
                            var respons = sdk.UpdateExperts(_token, expertid.id, clientid);
                            grdcalulat();
                        }
                    }
                    break;
                case "حذف":
                    {
                        int expertid = (grd.SelectedItem as ExpertDto).id;
                        var mess = FarsiMessageBox.MessageBox.Show("اخطار", "آیا از پاک کردن این بازدید اطمینان دارید؟", FarsiMessageBox.MessageBox.Buttons.YesNo, FarsiMessageBox.MessageBox.Icons.Warning);
                        if (mess==System.Windows.Forms.DialogResult.Yes)
                        {
                            var respons = sdk.DeleteExperts(_token, expertid);
                            if(respons.Result.IsSuccess)
                                grdcalulat();
                        }
                    }
                    break;
                case "صدور پیش فاکتور":
                    {
                        var expertid = (grd.SelectedItem as ExpertDto);
                        WpfApp1.Pages.Factor.Factor factor = new Factor.Factor(_token, 0, expertid.Client_ID, expertid.id);
                        factor.Owner = this;
                        factor.Show();
                        grdcalulat();
                    }
                    break;
              

            }
        }

        private void RadWindow_Activated(object sender, EventArgs e)
        {
            grdcalulat();
        }
    }
}
