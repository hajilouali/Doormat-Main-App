using DataLayer.Api.Response;
using PhoenixFutureApiSdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1.Pages.Products
{
    /// <summary>
    /// Interaction logic for ProductMain.xaml
    /// </summary>
    public partial class ProductMain : Window
    {
        public string token;
        FutureSdk sdk = new FutureSdk("https://localhost:5001");
        public ProductMain()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var products = sdk.GetAllProducts(token).Result;
            if (products.IsSuccess)
            {
                dg.ItemsSource = products.Data;
                
            }
                

        }




        #region Utility
        public class EnumBindingSourceExtension : MarkupExtension
        {
            private Type _enumType;
            public Type EnumType
            {
                get { return this._enumType; }
                set
                {
                    if (value != this._enumType)
                    {
                        if (null != value)
                        {
                            Type enumType = Nullable.GetUnderlyingType(value) ?? value;
                            if (!enumType.IsEnum)
                                throw new ArgumentException("Type must be for an Enum.");
                        }

                        this._enumType = value;
                    }
                }
            }

            public EnumBindingSourceExtension() { }

            public EnumBindingSourceExtension(Type enumType)
            {
                this.EnumType = enumType;
            }

            public override object ProvideValue(IServiceProvider serviceProvider)
            {
                if (null == this._enumType)
                    throw new InvalidOperationException("The EnumType must be specified.");

                Type actualEnumType = Nullable.GetUnderlyingType(this._enumType) ?? this._enumType;
                Array enumValues = Enum.GetValues(actualEnumType);

                if (actualEnumType == this._enumType)
                    return enumValues;

                Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
                enumValues.CopyTo(tempArray, 1);
                return tempArray;
            }
        }
        public void dg_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }

        }

        public static string GetPropertyDisplayName(object descriptor)
        {

            PropertyDescriptor pd = descriptor as PropertyDescriptor;
            if (pd != null)
            {
                DisplayNameAttribute dn = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
                if (dn != null && dn != DisplayNameAttribute.Default)
                {
                    return dn.DisplayName;
                }
            }
            else
            {
                PropertyInfo pi = descriptor as PropertyInfo;
                if (pi != null)
                {
                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        DisplayNameAttribute dn = attributes[i] as DisplayNameAttribute;
                        if (dn != null && dn != DisplayNameAttribute.Default)
                        {
                            return dn.DisplayName;
                        }
                    }
                }
            }
            return null;
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (dg.SelectedCells.Count > 0)
            {
                var dataRow = dg.SelectedItem as ProductAndServiceDto;
                //int index = dg.CurrentCell.Column.DisplayIndex;
                //string cellValue = dataRow.Row.ItemArray[index].ToString();
                MessageBox.Show(dataRow.id.ToString());
            }
                
            MessageBox.Show("لطفا یک محصول را جهت ویرایش انتخاب نمایید");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.token = token;
            product.Title = "افزودن محصول جدید";
            product.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            product.ShowDialog();
            
        }
    }
}
