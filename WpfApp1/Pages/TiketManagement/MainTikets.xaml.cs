using DataLayer.Api.Request;
using DataLayer.Api.Response;
using DoormatSite.Tools;
using MD.PersianDateTime;
using PersianDate;
using PhoenixFutureApiSdk;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ConversationalUI;

namespace Doormat.Pages.TiketManagement
{
    /// <summary>
    /// Interaction logic for MainTikets.xaml
    /// </summary>
    /// 
    //public class TaskBoardCardModel
    //{
    //    public string Assignee { get; set; }
    //    public string Description { get; set; }
    //    public string State { get; set; }
    //    public string Title { get; set; }
    //    public string CategoryName { get; set; }
    //}
    public partial class MainTikets
    {
        private string _token { get; set; }
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public MainTikets(string token)
        {
            _token = token;
            InitializeComponent();
           

            this.txtendAt.Culture = new System.Globalization.CultureInfo("fa");
            this.txtendAt.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            this.txtendAt.SelectedDate = DateTime.Now;
            this.txtstartAt.Culture = new System.Globalization.CultureInfo("fa");
            this.txtstartAt.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            PersianCalendar pc = new PersianCalendar();
            this.txtstartAt.SelectedDate = new DateTime(pc.GetYear(DateTime.Now), pc.GetMonth(DateTime.Now), 01, pc);
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 59), DispatcherPriority.Normal, delegate
            {
                binddata();
            }, this.Dispatcher);
            var alert = new RadDesktopAlert();
            alert.Header = "MAIL NOTIFICATION";
            alert.Content = "Hello, Here are two things that we noticed today on our daily meeting.";
            alert.ShowDuration = 3000;
            
            RadDesktopAlertManager manager = new RadDesktopAlertManager();
            manager.ShowAlert(alert);
        }

        void binddata()
        {
            if (EnableDate.IsChecked==true)
            {
                var respons = sdk.GetTikets(_token);
                if (respons.IsCompleted)
                {
                    grd.ItemsSource = respons.Result.Data;
                }
            }
            else
            {
                if (txtendAt.SelectedDate.Value>= txtstartAt.SelectedDate.Value)
                {
                    var dto = new gettiketbydate()
                    {
                        StartDate = txtstartAt.SelectedDate.Value.ToString("yyyy/MM/dd"),
                        EndDate = txtendAt.SelectedDate.Value.ToString("yyyy/MM/dd")
                    };
                    var respons = sdk.GetTiketsBtyPageid(_token, dto);
                    if (respons.IsCompleted)
                    {

                        grd.ItemsSource = respons.Result.Data;
                    }
                }
                else
                {
                    FarsiMessageBox.MessageBox.Show("اخطار", "لطفا تاریخ را اصلاح نمایید", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Warning);
                }
                
            }
            
        }
        private void grd_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            if (e.Row.DataContext is TiketDto)
            {
                e.Row.Background = (e.Row.DataContext as TiketDto).Closed == false ? new SolidColorBrush(Colors.LightGreen) : new SolidColorBrush(Colors.White);
            }
        }
        private void grd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = grd.SelectedItem as TiketDto;
            Chat chat = new Chat(_token,item);
            chat.Owner = this;
            chat.ShowDialog();
            binddata();
        }

        private void EnableDate_Checked(object sender, RoutedEventArgs e)
        {
            if (txtstartAt!=null)
            {
                txtstartAt.IsEnabled = false;
                txtendAt.IsEnabled = false;
                binddata();
            }
        }

        private void txtstartAt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            binddata();
        }

        private void EnableDate_Unchecked(object sender, RoutedEventArgs e)
        {
            txtstartAt.IsEnabled = true;
            txtendAt.IsEnabled = true;
            binddata();
        }
    }
}



