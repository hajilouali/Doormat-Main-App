using DoormatSite.Tools;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ServerSetting.xaml
    /// </summary>
    public partial class ServerSetting
    {
        public ServerSetting()
        {
            InitializeComponent();
            var address = xml.loadline("serverApi");
            txtserver.Text = address;
        }

        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            var address = txtserver.Text.ToLower().Trim();
            xml.SavetoXml("serverApi", address);
        }
    }
}
