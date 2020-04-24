using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Telerik.Windows.Controls;

namespace WpfApp1.Reposts.Factor
{
    /// <summary>
    /// Interaction logic for Factorview.xaml
    /// </summary>
    public partial class Factorview
    {
        public int FactorId { get; set; }
        public Factorview()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFactorNumber.Text))
            {
                FactorId = Int32.Parse(txtFactorNumber.Text);
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("لطفا شماره فاکتور را وارد نمایید");
            }
        }

        private void txtFactorNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }
    }
}
