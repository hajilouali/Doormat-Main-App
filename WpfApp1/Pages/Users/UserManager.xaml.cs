using DataLayer.Api.Request;
using DataLayer.Api.Response;
using DoormatSite.Tools;
using PhoenixFutureApiSdk;
using System;
using System.Collections.Generic;
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

namespace WpfApp1.Pages.Users
{
    /// <summary>
    /// Interaction logic for UserManager.xaml
    /// </summary>
    public partial class UserManager
    {
        private string _token;
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public UserModel model { get; set; }
        public UserManager(string token, UserModel M)
        {
            _token = token;
            model = M;
            InitializeComponent();
            this.Header = "افزودن کاربر";
            if (model != null)
            {
                this.Header = "ویرایش کاربر";
                txtFullname.Text = model.FullName;
                txtUsername.Text = model.UserName;
                txtemail.Value = model.Email;
                if (model.IsActive)
                {
                    ckisactive.IsChecked = true;
                }
            }
        }

        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtemail.Text) || string.IsNullOrEmpty(txtFullname.Text) || string.IsNullOrEmpty(txtUsername.Text))
            {
                FarsiMessageBox.MessageBox.Show("اخطار", "لطفا مغادیر خواسته شده را وارد نمایید", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Warning);

            }
            else
            {

                UserDto dto = new UserDto()
                {
                    Email = txtemail.Value,
                    FullName = txtFullname.Text,
                    UserName = txtUsername.Text,
                    IsActive = false,
                    Password = "123456789"
                };
                if (ckisactive.IsChecked == true)
                {
                    dto.IsActive = true;
                }
                if (model == null)
                {
                    var respons = sdk.addUser(_token, dto);
                    if (respons.Result.IsSuccess)
                    {
                        this.Close();

                    }
                    else
                    {
                        FarsiMessageBox.MessageBox.Show("اخطار", "در فرایندثبت کاربر مشکلی پیش آمده", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Warning);

                    }
                }
                else
                {
                    var respons = sdk.updateUser(_token, model.id, dto);
                    if (respons.Result.IsSuccess)
                    {
                        this.Close();

                    }
                    else
                    {
                        FarsiMessageBox.MessageBox.Show("اخطار", "در فرایندبروزرسانی کاربر مشکلی پیش آمده", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Warning);

                    }
                }




            }

        }

    }
}

