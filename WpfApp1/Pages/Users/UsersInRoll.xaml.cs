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

namespace WpfApp1.Pages.Users
{
    /// <summary>
    /// Interaction logic for UsersInRoll.xaml
    /// </summary>
    public partial class UsersInRoll
    {
        private string _token;
        private string _rollname;
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
        public UsersInRoll(string token, string rollname)
        {
            _token = token;
            _rollname = rollname;
            InitializeComponent();
            Griditemsurs();
            InitializeRowContextMenuItems();
        }
        private void Griditemsurs()
        {
            var allusers = sdk.GetAllUsers(_token).Result.Data;
            var usersinroll = sdk.GetUsersInRoll(_rollname, _token);
            if (usersinroll.Result.IsSuccess)
            {
                grd.ItemsSource = usersinroll.Result.Data;
            }
            var lsit = new List<UserModel>();
            foreach (var item in allusers)
            {
                if (!usersinroll.Result.Data.Where(p => p.id == item.id).Any())
                {
                    lsit.Add(item);
                }
            }

            RadComboBox1.ItemsSource = lsit;
        }

        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            if (RadComboBox1.SelectedItem != null)
            {
                var user = (RadComboBox1.SelectedItem as UserModel);

                var respons = sdk.addUserstoRolls(user.id, _rollname, _token);
                if (respons.Result.IsSuccess)
                {
                    Griditemsurs();
                }

            }


        }
        private void InitializeRowContextMenuItems()
        {
            ObservableCollection<WpfApp1.Pages.Factor.MenuItem> items = new ObservableCollection<WpfApp1.Pages.Factor.MenuItem>();
            WpfApp1.Pages.Factor.MenuItem Item2 = new WpfApp1.Pages.Factor.MenuItem();
            Item2.Text = "حذف از لیست";
            items.Add(Item2);

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

        }

        private void GridContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {

            WpfApp1.Pages.Factor.MenuItem item = (e.OriginalSource as RadMenuItem).DataContext as WpfApp1.Pages.Factor.MenuItem;
            switch (item.Text)
            {
                case "حذف از لیست":
                    {
                        if (grd.SelectedItem!=null)
                        {
                            var MessageBox = FarsiMessageBox.MessageBox.Show("اخطار", "آیا از پاک کردن این کاربر از لیست اطمینان دارید ؟", FarsiMessageBox.MessageBox.Buttons.YesNo, FarsiMessageBox.MessageBox.Icons.Warning);
                            if (MessageBox == System.Windows.Forms.DialogResult.Yes)
                            {
                                var UserModel = (grd.SelectedItem as UserModel);
                                var respons = sdk.removeUsersfromRolls(UserModel.id, _rollname, _token);
                                if (respons.Result.IsSuccess)
                                {
                                    Griditemsurs();
                                }
                            }
                        }
                        
                    }
                    break;


            }

        }

    }
}
