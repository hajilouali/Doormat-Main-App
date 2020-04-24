using DataLayer.Api.Request;
using DoormatSite.Tools;
using PhoenixFutureApiSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
            TokenPost tokenpost = new TokenPost()
            {
                username = lblUserName.Text,
                password = lblPassword.Password,
                grant_type = "password"
            };
            var respons = sdk.GetToken(tokenpost);
            if (respons.Result.IsSuccess)
            {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.token = respons.Result.Data.access_token;
                mainWindow.Show();
            }
            else
            {
                FarsiMessageBox.MessageBox.Show("اخطار",respons.Result.Message , FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Warning);
            }
        }
    }
}
