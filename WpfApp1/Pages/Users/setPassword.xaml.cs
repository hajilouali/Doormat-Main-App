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
    /// Interaction logic for setPassword.xaml
    /// </summary>
    public partial class setPassword
    {
        public string NewPassword { get; set; }
        public setPassword()
        {
            InitializeComponent();
        }

        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(newpassword.Password))
            {
                NewPassword = newpassword.Password;
                this.Close();
            }
            else
            {
                FarsiMessageBox.MessageBox.Show("اخطار", "لطفا رمز عبور خود را وارد نمایید", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Warning);
            }
        }
    }
}
