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
using WpfApp1.Pages.Factor;
using WpfApp1.RepostFormTools;
using WpfApp1.RepostFormTools.ViewModel;

namespace WpfApp1.Pages.Sanad
{
    /// <summary>
    /// Interaction logic for AddSanad.xaml
    /// </summary>
    public partial class AddSanad
    {
        private string _token;
        public int sanadid ;
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public ObservableCollection<AccountingHeading> AccountingHeading { get; set; }
        public ObservableCollection<SanadViewModel> Rows { get; set; }

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
        public AddSanad(string token,int id=0)
        {
            sanadid = id;
            _token = token;
            InitializeComponent();
            InitializeRowContextMenuItems();
            var AccountingHeadin = sdk.GetAccountingHeading(_token).Result;
            this.txtDate.Culture = new System.Globalization.CultureInfo("fa");
            this.txtDate.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            txtDate.SelectedDate = DateTime.Now;
            if (AccountingHeadin.IsSuccess)
            {
                var p = sdk.GetAccountingHeading(_token);
                if (p.Result.IsSuccess)
                {
                    ObservableCollection<AccountingHeading> list = new ObservableCollection<AccountingHeading>();
                    foreach (var item in p.Result.Data)
                    {
                        var ppp = new AccountingHeading();
                        ppp.AccountingType = item.AccountingType;
                        ppp.Discription = item.Discription;
                        ppp.id = item.id;
                        ppp.Title = item.Title;

                        list.Add(ppp);
                    }
                    AccountingHeading = list;
                }
            }
            
            if (sanadid != 0)
            {
                var factor = sdk.GetSanadById(token, sanadid);
                if (factor.Result.IsSuccess)
                {
                    
                    var rows = new System.Collections.ObjectModel.ObservableCollection<SanadViewModel>();
                    foreach (var item in factor.Result.Data.Sanads)
                    {
                        var p = new SanadViewModel();
                        p.AccountingHeading_ID = item.AccountingHeading_ID;
                        p.Bedehkari = item.Bedehkari;
                        p.Bestankari = item.Bestankari;
                        p.Comment = item.Comment;
                        
                        rows.Add(p);
                    }
                    Rows = rows;                    
                }
                int y, m, d;
                string[] list = new string[3];
                list = factor.Result.Data.StringDateTime.Split('/');
                y = int.Parse(list[0]);
                m = int.Parse(list[1]);
                d = int.Parse(list[2]);
                PersianCalendar pc = new PersianCalendar();
                txtDate.SelectedDate = new DateTime(y, m, d, pc);
              
            }

            bind();
            
        }
        private void bind()
        {
            if (Rows == null)
            {
                var rows = new System.Collections.ObjectModel.ObservableCollection<SanadViewModel>();
                rows.Add(new SanadViewModel() { AccountingHeading_ID = 7 });
                Rows = rows;
            }
            grdsanads.ItemsSource = Rows;
            ((GridViewComboBoxColumn)this.grdsanads.Columns[1]).ItemsSource = AccountingHeading;
        }

        private void grdsanads_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void grdsanads_TextChanged(object sender, TextChangedEventArgs e)
        {
            var cellnuber = grdsanads.SelectedCells.SingleOrDefault().Column.DisplayIndex;
            switch (cellnuber)
            {
                case 1:
                    {
                        var productingrid = new ObservableCollection<SanadViewModel>();
                        foreach (var item in grdsanads.Items)
                        {
                            productingrid.Add(item as SanadViewModel);
                        }

                        var rowIndex = this.grdsanads.Items.IndexOf(this.grdsanads.CurrentCellInfo.Item);
                        FindAccountingHeading radWindow1 = new FindAccountingHeading(_token);
                        radWindow1.Owner = this;
                        if (radWindow1.ShowDialog() == true)
                        {
                            productingrid[rowIndex].AccountingHeading_ID = radWindow1.AccountingHeading_Id;

                        }
                        Rows = productingrid;
                        grdsanads.ItemsSource = Rows;
                        CalculateRows();
                        break;
                    }
            }
            bind();
        }

        private void grdsanads_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                var row = new SanadViewModel();
                var productingrid = new ObservableCollection<SanadViewModel>();
                foreach (var item in grdsanads.Items)
                {
                    productingrid.Add(item as SanadViewModel);
                }


                FindAccountingHeading radWindow1 = new FindAccountingHeading(_token);
                radWindow1.Owner = this;
                if (radWindow1.ShowDialog() == true)
                {
                    row.AccountingHeading_ID = radWindow1.AccountingHeading_Id;

                }
                if (row.AccountingHeading_ID!=0)
                {
                    productingrid.Add(row);
                }
                
                Rows = productingrid;
                grdsanads.ItemsSource = Rows;
                CalculateRows();
                bind();

            }
        }

        private void GridContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (this.ClickedHeader != null)
            {
                foreach (var item in this.rowContextMenuItems)
                {
                    if (!item.Text.Equals("افزودن") && !item.Text.Equals("بروز رسانی"))
                    {
                        item.IsEnabled = false;
                    }

                }
                this.GridContextMenu.ItemsSource = this.rowContextMenuItems;
            }
            else if (this.ClickedRow != null)
            {
                this.grdsanads.SelectedItem = this.ClickedRow.DataContext;
                foreach (var item in this.rowContextMenuItems)
                {

                    item.IsEnabled = true;
                    if (item.Text.Equals("حذف ردیف"))
                    {
                        try
                        {
                            var ob = grdsanads.CurrentCellInfo.Item;
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
                    if (!item.Text.Equals("افزودن") && !item.Text.Equals("بروز رسانی"))
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
                case "افزودن":
                    {
                        var row = new SanadViewModel();
                        var productingrid = new ObservableCollection<SanadViewModel>();
                        foreach (var items in grdsanads.Items)
                        {
                            productingrid.Add(items as SanadViewModel);
                        }


                        FindAccountingHeading radWindow1 = new FindAccountingHeading(_token);
                        radWindow1.Owner = this;
                        if (radWindow1.ShowDialog() == true)
                        {
                            row.AccountingHeading_ID = radWindow1.AccountingHeading_Id;

                        }
                        if (row.AccountingHeading_ID != 0)
                        {
                            productingrid.Add(row);
                        }

                        Rows = productingrid;
                        grdsanads.ItemsSource = Rows;
                        CalculateRows();
                        bind();
                    }
                    break;
                case "بروز رسانی":
                    {
                        CalculateRows();
                    }
                    break;
                case "حذف ردیف":
                    {

                        var objec = grdsanads.CurrentCellInfo.Item;
                        this.grdsanads.Items.Remove(objec);
                    }
                    break;

            }
        }



        #region Metods
        private void InitializeRowContextMenuItems()
        {
            ObservableCollection<WpfApp1.Pages.Factor.MenuItem> items = new ObservableCollection<WpfApp1.Pages.Factor.MenuItem>();
            WpfApp1.Pages.Factor.MenuItem addItem = new WpfApp1.Pages.Factor.MenuItem();
            addItem.Text = "افزودن";
            items.Add(addItem);

            WpfApp1.Pages.Factor.MenuItem deleteItem = new WpfApp1.Pages.Factor.MenuItem();
            deleteItem.Text = "حذف ردیف";
            items.Add(deleteItem);
            WpfApp1.Pages.Factor.MenuItem RefreshItem = new WpfApp1.Pages.Factor.MenuItem();
            RefreshItem.Text = "بروز رسانی";
            items.Add(RefreshItem);
            this.rowContextMenuItems = items;
        }
        private void CalculateRows()
        {
            var productingrid = new ObservableCollection<SanadViewModel>();
            decimal totalBestankari = 0;
            decimal totalBedehkari = 0;
            foreach (var item in grdsanads.Items)
            {
                var row = item as SanadViewModel;
                totalBestankari += row.Bestankari;
                totalBedehkari += row.Bedehkari;
                productingrid.Add(row);
            }            
            Rows = productingrid;
            grdsanads.ItemsSource = Rows;
            txtbedehkari.Content = totalBedehkari.ToString("n0");
            txtbestankari.Content = totalBestankari.ToString("n0");
        }
        #endregion

        private void btnrefresh_Click(object sender, RoutedEventArgs e)
        {
            CalculateRows();
        }

        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            busycontent.IsBusy = true;
            CalculateRows();
            decimal totalBestankari = 0;
            decimal totalBedehkari = 0;
            foreach (var item in grdsanads.Items)
            {
                var row = item as SanadViewModel;
                totalBestankari += row.Bestankari;
                totalBedehkari += row.Bedehkari;
                
            }
            if (totalBestankari == totalBedehkari)
            {
                var sanadheading =new sanadViewModel();
                sanadheading.Discription = txtDiscription.Text;
                sanadheading.ShamsiDate = txtDate.SelectedDate.Value.ToString("yyyy/MM/dd");
                List<SanadViewModel> sanads = new List<SanadViewModel>();
                foreach (var item in grdsanads.Items)
                {
                    sanads.Add(item as SanadViewModel);
                }
                sanadheading.Sanads = sanads;
                var messeage = MessageBox.Show("آیا از ثبت این سند اطمینان دارید ؟", "تایید ثبت", MessageBoxButton.OKCancel);
                if (messeage==MessageBoxResult.OK)
                {
                    if (sanadid!=0)
                    {
                        var result = sdk.EditSanad(_token, sanadid, sanadheading);
                        if (result.Result.IsSuccess)
                        {
                            List<SanadsReportsViewModel> model = new List<SanadsReportsViewModel>();
                            foreach (var item in result.Result.Data.Sanads)
                            {
                                model.Add(new SanadsReportsViewModel()
                                {
                                    AccountingHeading_ID = item.AccountingHeading_ID,
                                    AccountingHeading = item.AccountingHeading,
                                    Bedehkari = item.Bedehkari,
                                    Bestankari = item.Bestankari,
                                    Comment = item.Comment,
                                    DateTime = item.stringDatatime,
                                    Discription = result.Result.Data.Discription,
                                    Sanad_ID = result.Result.Data.id
                                });
                            }
                            SanadsReports.SanadsView(model);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("در فرایند ثبت سند مشکلی پیش آمده", "اخطار", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        var result = sdk.AddSanad(_token, sanadheading);
                        if (result.Result.IsSuccess)
                        {
                            List<SanadsReportsViewModel> model = new List<SanadsReportsViewModel>();
                            foreach (var item in result.Result.Data.Sanads)
                            {
                                model.Add(new SanadsReportsViewModel()
                                {
                                    AccountingHeading_ID = item.AccountingHeading_ID,
                                    AccountingHeading = item.AccountingHeading,
                                    Bedehkari = item.Bedehkari,
                                    Bestankari = item.Bestankari,
                                    Comment = item.Comment,
                                    DateTime = item.stringDatatime,
                                    Discription = result.Result.Data.Discription,
                                    Sanad_ID = result.Result.Data.id
                                });
                            }
                            SanadsReports.SanadsView(model);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("در فرایند ثبت سند مشکلی پیش آمده", "اخطار", MessageBoxButton.OK);
                        }
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("جمع ستون های بدهکاری و بستانکاری با یکدیگر مساوی نمی باشد.", "اخطار", MessageBoxButton.OK);
            }
            busycontent.IsBusy = false;
        }

        private void RadWindow_Activated(object sender, EventArgs e)
        {
            CalculateRows();
        }
    }
}
