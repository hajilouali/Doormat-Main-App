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
    /// Interaction logic for Rolls.xaml
    /// </summary>
    public partial class Rolls
    {
        private string _token;
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public Rolls(string token)
        {
            _token = token;
            InitializeComponent();
            var rolss = sdk.GetRolls(_token).Result.Data;
            grdroll.ItemsSource = rolss;
        }

        private void grdroll_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var roll = (grdroll.SelectedItem as Roll);
            UsersInRoll usersInRoll = new UsersInRoll(_token, roll.name);
            usersInRoll.Header = "اعضای گروه" + roll.name;
            usersInRoll.Owner = this;
            usersInRoll.ShowDialog();
        }
    }
}
