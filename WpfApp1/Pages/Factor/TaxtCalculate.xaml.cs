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

namespace WpfApp1.Pages.Factor
{
    /// <summary>
    /// Interaction logic for TaxtCalculate.xaml
    /// </summary>
    public partial class TaxtCalculate
    {
        public int Taxt { get; set; }
        public bool IsTaxt { get; set; }
        public TaxtCalculate()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            IsTaxt = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            IsTaxt = false;
            this.Close();
        }
    }
}
