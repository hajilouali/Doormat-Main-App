using DataLayer;
using MD.PersianDateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApp1.Pages.Products;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string token;
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                PersianDateTime persianDateTime = new PersianDateTime(DateTime.Now);
                //this.LblStatus.Content = DateTime.Now.ToString("ddd, dd MMM yyy - HH':'mm':'ss ");
                this.LblStatus.Content = persianDateTime.ToLongDateTimeString();
            }, this.Dispatcher);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataApi d = new DataApi();
            PhoenixFutureApiSdk.FutureSdk sdk = new PhoenixFutureApiSdk.FutureSdk("https://localhost:5001");

            var dateo = d.GetDateOccasion();
            if (dateo.Result.type == DataLayer.Api.typeDateRespuns.success)
            {
                if (dateo.Result.values.Count != 0)
                {
                    Lbldateoc.Content = "مناسبت های روز :";
                    foreach (var item in dateo.Result.values)
                    {
                        Lbldateoc.Content += item.occasion + ",";
                    }
                }
            }
            var user = sdk.GetUserBio(token).Result;
            lblusername.Content = " کاربر :"+ user.Data.FullName;
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductMain productMain = new ProductMain();
            productMain.token = token;
            productMain.ShowDialog();
        }
    }
}
