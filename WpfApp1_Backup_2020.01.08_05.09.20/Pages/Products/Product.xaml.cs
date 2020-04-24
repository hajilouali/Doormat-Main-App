using DataLayer.Api.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
using Telerik.Windows.Data;

namespace WpfApp1.Pages.Products
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        public string token;
        public int ProductId;
        public Product()
        {
            InitializeComponent();
            this.DataContext = new ProductAddViewModel();
            
        }

       


    }
    
}
