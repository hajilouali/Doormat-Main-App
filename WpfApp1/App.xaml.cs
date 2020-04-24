using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fa");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fa");
            LocalizationManager.Manager = new CustomLocalizationManager();
            InitializeComponent();
        }
    }
}
