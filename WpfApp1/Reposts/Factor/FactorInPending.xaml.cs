using DataLayer.Api.Request;
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
using WpfApp1.RepostFormTools;

namespace WpfApp1.Reposts.Factor
{
    /// <summary>
    /// Interaction logic for FactorInPending.xaml
    /// </summary>
    public partial class FactorInPending
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
        private GridViewHeaderCell ClickedHeaderPartner
        {
            get
            {
                return this.GridContextMenuPartner.GetClickedElement<GridViewHeaderCell>();
            }
        }
        private GridViewRow ClickedRowPartner
        {
            get
            {
                return this.GridContextMenuPartner.GetClickedElement<GridViewRow>();
            }
        }
        public FactorInPending(string token)
        {
            _token = token;
            InitializeComponent();
            itemsurs();
            InitializeRowContextMenuItems();
        }
        private void itemsurs()
        {
            var dto = new GetFactor()
            {
                ISAllType = false,
                StartDate = "",
                EndDate = "",
                ClientID = 0,
                FactorType = DataLayer.Api.Response.FactorType.PendingToAccept,
            };
            var respons = sdk.GetAllFactor(_token, dto);
            if (respons.Result.IsSuccess)
            {
                var userspartner = sdk.GetUsersInRoll("Experts", _token).Result.Data;
                List<FactorDto> list = new List<FactorDto>();
                foreach (var item in respons.Result.Data)
                {
                    if (userspartner.Where(p => p.id == item.User_ID).Any())
                    {
                        list.Add(item);
                    }
                }
                grdpartner.ItemsSource = list;
                var listcellpartner = respons.Result.Data;
                foreach (var item in list)
                {
                    listcellpartner.Remove(item);
                }
                grdcellpartner.ItemsSource = listcellpartner;
            }
        }
        private void InitializeRowContextMenuItems()
        {
            ObservableCollection<WpfApp1.Pages.Factor.MenuItem> items = new ObservableCollection<WpfApp1.Pages.Factor.MenuItem>();
            WpfApp1.Pages.Factor.MenuItem addItem = new WpfApp1.Pages.Factor.MenuItem();
            addItem.Text = "نمایش فاکتور";
            items.Add(addItem);

            WpfApp1.Pages.Factor.MenuItem deleteItem = new WpfApp1.Pages.Factor.MenuItem();
            deleteItem.Text = "پیوست های فاکتور";
            items.Add(deleteItem);
            WpfApp1.Pages.Factor.MenuItem RefreshItem = new WpfApp1.Pages.Factor.MenuItem();
            RefreshItem.Text = "ویرایش فاکتور";
            items.Add(RefreshItem);
            WpfApp1.Pages.Factor.MenuItem RefreshItema = new WpfApp1.Pages.Factor.MenuItem();
            RefreshItema.Text = "حذف فاکتور";
            items.Add(RefreshItema);
            WpfApp1.Pages.Factor.MenuItem RefreshItemb = new WpfApp1.Pages.Factor.MenuItem();
            RefreshItemb.Text = "تایید به فاکتور";
            items.Add(RefreshItemb);
            WpfApp1.Pages.Factor.MenuItem RefreshItemc = new WpfApp1.Pages.Factor.MenuItem();
            RefreshItemc.Text = "تایید به پیش فاکتور";
            items.Add(RefreshItemc);
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
                this.grdcellpartner.SelectedItem = this.ClickedRow.DataContext;
                foreach (var item in this.rowContextMenuItems)
                {

                    item.IsEnabled = true;
                    if (item.Text.Equals("تایید به پیش فاکتور"))
                    {
                        item.IsEnabled = false;
                    }
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
        private void GridContextMenu_OpenedPartner(object sender, RoutedEventArgs e)
        {
            if (this.ClickedHeaderPartner != null)
            {
                foreach (var item in this.rowContextMenuItems)
                {

                    item.IsEnabled = false;


                }
                this.GridContextMenuPartner.ItemsSource = this.rowContextMenuItems;
            }
            else if (this.ClickedRowPartner != null)
            {
                this.grdcellpartner.SelectedItem = this.ClickedRow.DataContext;
                foreach (var item in this.rowContextMenuItems)
                {

                    item.IsEnabled = true;
                    if (item.Text.Equals("تایید به فاکتور"))
                    {
                        
                                item.IsEnabled = false;
                          

                    }
                }
                this.GridContextMenuPartner.ItemsSource = this.rowContextMenuItems;
            }
            else
            {
                foreach (var item in this.rowContextMenuItems)
                {

                    item.IsEnabled = false;




                }
                this.GridContextMenuPartner.ItemsSource = this.rowContextMenuItems;
            }
        }
        private void GridContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            WpfApp1.Pages.Factor.MenuItem item = (e.OriginalSource as RadMenuItem).DataContext as WpfApp1.Pages.Factor.MenuItem;
            var Factordto = (grdcellpartner.SelectedItem as FactorDto);
            switch (item.Text)
            {
                case "نمایش فاکتور":
                    {
                        
                        FactorReports.FactorViewReport(Factordto,sdk.GetClientById(_token,Factordto.Client_ID).Result.Data);
                    }
                    break;
                case "پیوست های فاکتور":
                    {
                        WpfApp1.Pages.Factor.FactorAttachment factor = new WpfApp1.Pages.Factor.FactorAttachment(_token, Factordto.id);
                        factor.Owner = this;
                        factor.Show();
                    }
                    break;
                case "ویرایش فاکتور":
                    {

                        WpfApp1.Pages.Factor.Factor factor = new Pages.Factor.Factor(_token, Factordto.id);
                        factor.Owner = this;
                        factor.Header = "ویرایش فاکتور";
                        factor.Show();
                        itemsurs();
                    }
                    break;
                case "حذف فاکتور":
                    {
                        var message = FarsiMessageBox.MessageBox.Show("اخطار", "آیا از پاک کردن این فاکتور اطمینان دارید؟", FarsiMessageBox.MessageBox.Buttons.YesNo, FarsiMessageBox.MessageBox.Icons.Warning);
                        if (message==System.Windows.Forms.DialogResult.Yes)
                        {
                            var respons = sdk.deleteFactor(_token, Factordto.id);
                            itemsurs();
                        }
                        

                    }
                    break;
                case "تایید به فاکتور":
                    {
                        var message = FarsiMessageBox.MessageBox.Show("اخطار", "آیا از تبدیل این فاکتور اطمینان دارید؟", FarsiMessageBox.MessageBox.Buttons.YesNo, FarsiMessageBox.MessageBox.Icons.Warning);
                        if (message==System.Windows.Forms.DialogResult.Yes)
                        {
                            var respons = sdk.CheangToFactor(_token, Factordto.id);
                            FactorReports.FactorViewReport(Factordto, sdk.GetClientById(_token, Factordto.Client_ID).Result.Data);

                        }


                    }
                    break;

            }
        }

        private void GridContextMenuPartner_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            WpfApp1.Pages.Factor.MenuItem item = (e.OriginalSource as RadMenuItem).DataContext as WpfApp1.Pages.Factor.MenuItem;
            var Factordto = (grdpartner.SelectedItem as FactorDto);

            switch (item.Text)
            {
                case "نمایش فاکتور":
                    {

                        FactorReports.FactorViewReport(Factordto, sdk.GetClientById(_token, Factordto.Client_ID).Result.Data);
                    }
                    break;
                case "پیوست های فاکتور":
                    {
                        WpfApp1.Pages.Factor.FactorAttachment factor = new WpfApp1.Pages.Factor.FactorAttachment(_token, Factordto.id);
                        factor.Owner = this;
                        factor.Show();
                    }
                    break;
                case "ویرایش فاکتور":
                    {

                        WpfApp1.Pages.Factor.Factor factor = new Pages.Factor.Factor(_token, Factordto.id);
                        factor.Owner = this;
                        factor.Header = "ویرایش فاکتور";
                        factor.Show();
                        itemsurs();
                    }
                    break;
                case "حذف فاکتور":
                    {
                        var message = FarsiMessageBox.MessageBox.Show("اخطار", "آیا از پاک کردن این فاکتور اطمینان دارید؟", FarsiMessageBox.MessageBox.Buttons.YesNo, FarsiMessageBox.MessageBox.Icons.Warning);
                        if (message == System.Windows.Forms.DialogResult.Yes)
                        {
                            var respons = sdk.deleteFactor(_token, Factordto.id);
                            itemsurs();
                        }

                    }
                    break;
                case "تایید به پیش فاکتور":
                    {
                        var message = FarsiMessageBox.MessageBox.Show("اخطار", "آیا از تبدیل این فاکتور اطمینان دارید؟", FarsiMessageBox.MessageBox.Buttons.YesNo, FarsiMessageBox.MessageBox.Icons.Warning);
                        if (message == System.Windows.Forms.DialogResult.Yes)
                        {
                            var respons = sdk.CheangToPishFactor(_token, Factordto.id);
                            FactorReports.FactorViewReport(Factordto, sdk.GetClientById(_token, Factordto.Client_ID).Result.Data);

                        }


                    }
                    break;

            }
        }
    }
}
