using DataLayer.Api.Request;
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

namespace WpfApp1.Pages.Manufacture
{
    /// <summary>
    /// Interaction logic for AddManufacuryHistory.xaml
    /// </summary>
    public partial class AddManufacuryHistory
    {
        private string _token;
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        private int _Manufactory_ID { get; set; }
        IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> _ConditionManufactures;
        public IEnumerable<Telerik.Windows.Data.EnumMemberViewModel> ConditionManufactures
        {
            get
            {
                if (_ConditionManufactures == null)
                {
                    // Calling this static factory method will return an IEnumerable
                    // full of view models. The Value property of each view model is
                    // the actual enum value, so you would typically set 
                    // RadComboBox.SelectedValueMemberPath to be equal to "Value". 
                    // The DisplayName property of each view model returns the first 
                    // of the following properties that is not null:
                    // - DisplayShortName (Silverlight only)
                    // - Description
                    // - Name
                    // So you would typically set the DisplayMemberPath of the combo to
                    // be equal to "DisplayName". But you can use any of the other
                    // properties of the view model if you want.

                    _ConditionManufactures = Telerik.Windows.Data.EnumDataSource.FromType<ConditionManufacture>();
                }

                return _ConditionManufactures;
            }
        }
        public AddManufacuryHistory(string token,int Manufactory_ID)
        {
            _token = token;
            _Manufactory_ID = Manufactory_ID;
            InitializeComponent();
            this.DataContext = this;
        }

        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            if (Vaziyat.SelectedItem!=null)
            {
                var item = (ConditionManufacture)Vaziyat.SelectedValue;
                var dto = new ManufactureHistoryAdd() {
                ConditionManufacture= item,
                Discription=txtDiscription.Text,
                Manufactury_ID= _Manufactory_ID
                };
                var respons = sdk.addManufactureHistury(_token, dto);
                if (respons.Result.IsSuccess)
                {
                    this.Close();

                }
                else
                {
                    FarsiMessageBox.MessageBox.Show("اخطار", "خطار در پردازش اطلاعات", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Error);

                }
            }
            else 
            {
                FarsiMessageBox.MessageBox.Show("اخطار", "لطفا وضعیت را انتخاب نمایید", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Error);
            }
        }
    }
}
