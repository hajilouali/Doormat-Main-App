using DataLayer.Api.Response;
using DataLayer.model;
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
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users
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
        public Users(string token)
        {
            _token = token;
            InitializeComponent();
            GrdRow();
            InitializeRowContextMenuItems();
        }
        private void GrdRow()
        {

            var respons = sdk.GetAllUsers(_token);
            if (respons.Result.IsSuccess)
            {
                var list = respons.Result.Data;
                var rolss = sdk.GetRolls(_token).Result.Data;
                List<UsersByRoll> lists = new List<UsersByRoll>();
                foreach (var item in list)
                {
                    List<Roll> rolssss = new List<Roll>();
                    foreach (var items in item.RollName)
                    {
                        rolssss.Add(rolss.Where(p => p.name == items).SingleOrDefault());
                    }
                    lists.Add(new UsersByRoll()
                    {
                        Email = item.Email,
                        FullName = item.FullName,
                        id = item.id,
                        IsActive = item.IsActive,
                        UserName = item.UserName,
                        Rolls = rolssss
                    });
                }
                grd.ItemsSource = lists;
            }
        }

        private void btnnew_Click(object sender, RoutedEventArgs e)
        {
            UserManager userManager = new UserManager(_token, null);
            userManager.Owner = this;
            userManager.ShowDialog();
        }

        private void RadWindow_Activated(object sender, EventArgs e)
        {
            GrdRow();
        }

        private void btnRollManager_Click(object sender, RoutedEventArgs e)
        {
            Rolls rolls = new Rolls(_token);
            rolls.Owner = this;
            rolls.ShowDialog();
        }

        private void grd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        private void InitializeRowContextMenuItems()
        {
            ObservableCollection<WpfApp1.Pages.Factor.MenuItem> items = new ObservableCollection<WpfApp1.Pages.Factor.MenuItem>();
            WpfApp1.Pages.Factor.MenuItem Item2 = new WpfApp1.Pages.Factor.MenuItem();
            Item2.Text = "حذف کاربر";
            items.Add(Item2);
            WpfApp1.Pages.Factor.MenuItem Item1 = new WpfApp1.Pages.Factor.MenuItem();
            Item1.Text = "ویرایش کاربر";
            items.Add(Item1);
            WpfApp1.Pages.Factor.MenuItem Item3 = new WpfApp1.Pages.Factor.MenuItem();
            Item3.Text = "تغییر رمز عبور";
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
            var UserModel = (grd.SelectedItem as UsersByRoll);

            switch (item.Text)
            {
                case "حذف کاربر":
                    {
                        if (grd.SelectedItem != null)
                        {
                            var MessageBox = FarsiMessageBox.MessageBox.Show("اخطار", "آیا از پاک کردن این کاربر از اطمینان دارید ؟", FarsiMessageBox.MessageBox.Buttons.YesNo, FarsiMessageBox.MessageBox.Icons.Warning);
                            if (MessageBox == System.Windows.Forms.DialogResult.Yes)
                            {
                                var respons = sdk.deletUser(_token, UserModel.id);
                                if (respons.Result.IsSuccess)
                                {
                                    GrdRow();
                                }
                                else
                                {
                                    FarsiMessageBox.MessageBox.Show("اخطار", "متاسفانه امکان حذف این کاربر در حال حاضر موجود نمی باشد.", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Warning);
                                }
                            }
                        }

                    }
                    break;
                case "ویرایش کاربر":
                    {
                        UserManager userManager = new UserManager(_token, new DataLayer.Api.Response.UserModel()
                        {
                            Email = UserModel.Email,
                            FullName = UserModel.FullName,
                            id = UserModel.id,
                            IsActive = UserModel.IsActive,
                            UserName = UserModel.UserName
                        });
                        userManager.Owner = this;

                        userManager.txtPassword.IsEnabled = false;
                        userManager.Header = "ویرایش کاربر";
                        userManager.ShowDialog();

                    }
                    break;
                case "تغییر رمز عبور":
                    {
                        setPassword setPassword = new setPassword();
                        setPassword.Owner = this;
                        setPassword.ShowDialog();
                        var password = setPassword.NewPassword;
                        if (!string.IsNullOrEmpty(password))
                        {
                            var respons = sdk.setnewpasswordUser(_token, UserModel.id, password);
                            if (respons.Result.IsSuccess)
                            {
                                FarsiMessageBox.MessageBox.Show("", "رمز عبور با موفقیت تغییر یافت", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Information);

                            }
                            else
                            {
                                FarsiMessageBox.MessageBox.Show("اخطار", "عملیات ناموفق", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Error);

                            }
                        }


                    }
                    break;

            }

        }
    }
}
