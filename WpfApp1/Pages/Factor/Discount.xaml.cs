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

namespace WpfApp1.Pages.Factor
{
    /// <summary>
    /// Interaction logic for Discount.xaml
    /// </summary>
    public partial class Discount
    {
        public decimal Descounts { get; set; }
        public int DescountPersents { get; set; }
        public decimal Price { get; set; }
        public Discount()
        {
            InitializeComponent();
            this.DataContext = this;
           
            
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        

        private void DescountPersent_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (DescountPersent.Text != null)
                {
                    var s = Convert.ToDouble(DescountPersent.Text);
                    var percent =Convert.ToDecimal(s) / 100;
                    var value = Price * percent;
                    Descount.Text = value.ToString("n0");
                    Descounts =Convert.ToInt32(value);

                    DescountPersents =Convert.ToInt32(s);
                }
                
            }
            catch (Exception)
            {
                Descounts = 0;
                DescountPersents = 0;
                Descount.Text = "0";
            }
            

           
        }

        private void DescountPersent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void Descount_TextChanged(object sender, TextChangedEventArgs e)
        {
            //try
            //{
            //    DescountPersents = 0;
            //    DescountPersent.Text = "0";
            //    var s = Convert.ToDouble(Descount.Text);
            //    Descounts = Convert.ToInt32(s);
            //}
            //catch (Exception)
            //{

            //    DescountPersents = 0;
            //    DescountPersent.Text = "0";
            //    Descount.Text="0";
            //    Descounts = 0;
            //}
        }
    }
}
