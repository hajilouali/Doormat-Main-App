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
using WpfApp1.Pages.Factor;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using WpfApp1.RepostFormTools;
using System.Windows.Forms;
using FactorAttachment = WpfApp1.Pages.Factor.FactorAttachment;

namespace WpfApp1.Reposts.Accounting
{
    /// <summary>
    /// Interaction logic for Factors.xaml
    /// </summary>
    public partial class Factors
    {
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
        private string _token;

        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> _factorType;
        public IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> FactorTypes
        {
            get
            {
                if (_factorType == null)
                {
                    _factorType = Telerik.Windows.Data.EnumDataSource.FromType<FactorType>();
                }

                return _factorType;
            }
        }
        public Factors(string token)
        {
            InitializeComponent();
            InitializeRowContextMenuItems();
            this.DataContext = this;
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
                var dto = new GetFactor();
                dto.StartDate = "";
                dto.EndDate = "";
                dto.ISAllType = true;
                dto.ClientID = 0;
                if (EnableDate.IsChecked == true)
                {
                    dto.StartDate = txtstartAt.SelectedValue.Value.ToString("yyyy/MM/dd");
                    dto.EndDate = txtendAt.SelectedValue.Value.ToString("yyyy/MM/dd");
                }
                
                var respons = sdk.GetAllFactor(_token, dto);
                if (respons.Result.IsSuccess)
                {
                    grd.ItemsSource = respons.Result.Data;
                    decimal mandeh = 0; 
                    foreach (var item in respons.Result.Data)
                    {
                        mandeh += item.FactorPrice;
                    }
                    txtmandeh.Text = mandeh.ToString("n0");
                }

            }
        }

        private void RadWindow_Activated(object sender, EventArgs e)
        {
            gridItemSurce();
        }

        private void EnableDate_Checked(object sender, RoutedEventArgs e)
        {
            gridItemSurce();
        }

        private void EnableDate_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void ReportGrid_Click(object sender, RoutedEventArgs e)
        {

        }

        private void grd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtstartAt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gridItemSurce();
        }
        private void InitializeRowContextMenuItems()
        {
            ObservableCollection<WpfApp1.Pages.Factor.MenuItem> items = new ObservableCollection<WpfApp1.Pages.Factor.MenuItem>();
            WpfApp1.Pages.Factor.MenuItem Item2 = new WpfApp1.Pages.Factor.MenuItem();
            Item2.Text = "نمایش فاکتور";
            items.Add(Item2);
            WpfApp1.Pages.Factor.MenuItem Item4 = new WpfApp1.Pages.Factor.MenuItem();
            Item4.Text = "پیوست های فاکتور";
            items.Add(Item4);
            WpfApp1.Pages.Factor.MenuItem addItem = new WpfApp1.Pages.Factor.MenuItem();
            addItem.Text = "فاکتور جدید";
            items.Add(addItem);
            WpfApp1.Pages.Factor.MenuItem Item1 = new WpfApp1.Pages.Factor.MenuItem();
            Item1.Text = "ویرایش فاکتور";
            items.Add(Item1);
            WpfApp1.Pages.Factor.MenuItem deleteItem = new WpfApp1.Pages.Factor.MenuItem();
            deleteItem.Text = "تبدیل به فاکتور";
            items.Add(deleteItem);
            WpfApp1.Pages.Factor.MenuItem RefreshItem = new WpfApp1.Pages.Factor.MenuItem();
            RefreshItem.Text = "تبدیل به  پیش فاکتور";
            items.Add(RefreshItem);
            WpfApp1.Pages.Factor.MenuItem Item3 = new WpfApp1.Pages.Factor.MenuItem();
            Item3.Text = "حذف فاکتور";
            items.Add(Item3);
            this.rowContextMenuItems = items;
        }
        private void GridContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (this.ClickedHeader != null)
            {
                foreach (var item in this.rowContextMenuItems)
                {
                    if (!item.Text.Equals("فاکتور جدید"))
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
                    if (item.Text.Equals("ویرایش فاکتور") || item.Text.Equals("نمایش فاکتور") || item.Text.Equals("حذف فاکتور") || item.Text.Equals("پیوست های فاکتور"))
                    {
                        try
                        {
                            var ob = grd.CurrentCellInfo.Item as FactorDto;
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
                    if (item.Text.Equals("تبدیل به فاکتور"))
                    {
                        try
                        {
                            var ob = grd.CurrentCellInfo.Item as FactorDto;
                            if (ob.FactorType == FactorType.Factor)
                            {
                                item.IsEnabled = false;
                            }

                        }
                        catch (Exception)
                        {

                            item.IsEnabled = false;
                        }

                    }
                    if (item.Text.Equals("تبدیل به  پیش فاکتور"))
                    {
                        try
                        {
                            var ob = grd.CurrentCellInfo.Item as FactorDto;
                            if (ob.FactorType == FactorType.PishFactor)
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
                    if (!item.Text.Equals("فاکتور جدید"))
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
                case "پیوست های فاکتور":
                    {
                        var opject = grd.CurrentCellInfo.Item as FactorDto;

                        FactorAttachment attachment = new FactorAttachment(_token, opject.id);
                        attachment.Owner = this;
                        attachment.ShowDialog();
                    }
                    break;
                case "نمایش فاکتور":
                    {
                        var opject = grd.CurrentCellInfo.Item as FactorDto;
                        var factor = sdk.GetFactorById(_token, opject.id);
                        if (factor.Result.IsSuccess)
                        {
                            FactorDto dto = factor.Result.Data;
                            var client = sdk.GetClientById(_token, dto.Client_ID);
                            FactorReports.FactorViewReport(dto, client.Result.Data);
                        }
                        else
                        {
                            FarsiMessageBox.MessageBox.Show("مشکلی در مشاهده فاکتور بوجود آماده", "", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Error);
                            
                        }
                    }
                    break;
                case "فاکتور جدید":
                    {
                        WpfApp1.Pages.Factor.Factor factor = new Pages.Factor.Factor(_token,0);
                        factor.Owner = this;
                        factor.ShowDialog();
                    }
                    break;
                case "ویرایش فاکتور":
                    {
                        var opject = grd.CurrentCellInfo.Item as FactorDto;
                        DialogResult mes = FarsiMessageBox.MessageBox.Show("ویرایش فاکتور", string.Format("آیا از ویرایش فاکتور شماره {0} اطمینان دارید ؟", opject.id), FarsiMessageBox.MessageBox.Buttons.YesNo, FarsiMessageBox.MessageBox.Icons.Question);
                        if (mes== System.Windows.Forms.DialogResult.Yes)
                        {
                            WpfApp1.Pages.Factor.Factor factor = new Pages.Factor.Factor(_token, opject.id);
                            factor.Header = "ویرایش فاکتور";
                            factor.Owner = this;
                            factor.ShowDialog();
                        }
                        
                    }
                    break;
                case "تبدیل به فاکتور":
                    {
                        var opject = grd.CurrentCellInfo.Item as FactorDto;
                        DialogResult mes = FarsiMessageBox.MessageBox.Show("تبدیل به فاکتور", string.Format("آیا از تبدیل فاکتور شماره {0} اطمینان دارید ؟", opject.id), FarsiMessageBox.MessageBox.Buttons.YesNo, FarsiMessageBox.MessageBox.Icons.Question);
                        if (mes == System.Windows.Forms.DialogResult.Yes)
                        {
                            var result = sdk.CheangToFactor(_token, opject.id);
                            if (result.Result.IsSuccess)
                            {
                                FactorDto dto = result.Result.Data;
                                var client = sdk.GetClientById(_token, dto.Client_ID);
                                FactorReports.FactorViewReport(dto, client.Result.Data);
                            }
                            else
                            {
                                FarsiMessageBox.MessageBox.Show("مشکلی در تغییر به فاکتور بوجود آماده", "", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Error);

                            }
                        }
                           
                    }
                    break;
                case "تبدیل به  پیش فاکتور":
                    {
                        var opject = grd.CurrentCellInfo.Item as FactorDto;
                        DialogResult mes = FarsiMessageBox.MessageBox.Show("تبدیل به پیش فاکتور", string.Format("آیا از تبدیل فاکتور شماره {0} اطمینان دارید ؟", opject.id), FarsiMessageBox.MessageBox.Buttons.YesNo, FarsiMessageBox.MessageBox.Icons.Question);
                        if (mes == System.Windows.Forms.DialogResult.Yes)
                        {
                            var result = sdk.CheangToPishFactor(_token, opject.id);
                            if (result.Result.IsSuccess)
                            {
                                FactorDto dto = result.Result.Data;
                                var client = sdk.GetClientById(_token, dto.Client_ID);
                                FactorReports.FactorViewReport(dto, client.Result.Data);
                            }
                            else
                            {
                                FarsiMessageBox.MessageBox.Show("مشکلی در تغییر به پیش فاکتور بوجود آماده", "", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Error);

                            }
                        }
                            

                    }
                    break;
                case "حذف فاکتور":
                    {
                        var opject = grd.CurrentCellInfo.Item as FactorDto;
                        DialogResult mes = FarsiMessageBox.MessageBox.Show("حذف فاکتور", string.Format("آیا از حدف فاکتور شماره {0} اطمینان دارید ؟", opject.id), FarsiMessageBox.MessageBox.Buttons.YesNo, FarsiMessageBox.MessageBox.Icons.Question);
                        if (mes == System.Windows.Forms.DialogResult.Yes)
                        {
                            
                            var result = sdk.deleteFactor(_token, opject.id);
                            if (result.Result.IsSuccess)
                            {


                            }
                            else
                            {
                                FarsiMessageBox.MessageBox.Show("مشکلی در تغییر به پیش فاکتور بوجود آماده", "", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Error);

                            }
                        }
                            
                    }
                    break;

            }
            gridItemSurce();
        }
    }
}
