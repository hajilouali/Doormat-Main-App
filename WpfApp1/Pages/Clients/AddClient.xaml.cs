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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Telerik.Windows.Controls;

namespace WpfApp1.Pages.Clients
{
    /// <summary>
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient
    {
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public int id = 0;
        public string token;
        private int ClientId; 
        public AddClient()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            ClientDto dto = new ClientDto()
            {

                id = id,
                ClientName = txtName.Text,
                ClientAddress = txtAddress.Text,
                ClientPartnerName = txtClientPartner.Text,
                ClientPhone = txtPhone.Text,
                CodeEgtesadi = txtCodeEgtesadi.Text,
                CodeMeli=txtCodeMeli.Text,
                DiscountPercent=(decimal)txtDiscountPercent.Value,
                MaxCreditValue= (double)txtMaxCreditValue.Value
            };
            if (id == 0)
            {
                if (sdk.GetAllClients(token).Result.Data.Where(p => p.ClientName == dto.ClientName).Any())
                {
                    var s = System.Windows.Forms.MessageBox.Show("کاربر اضافه شده تکراری می باشد. آیا از ثبت آن اطمینان دارید؟", "اخطار", MessageBoxButtons.OKCancel);
                    if (s == System.Windows.Forms.DialogResult.OK)
                    {
                        var result = sdk.AddClient(token, dto);
                        if (!result.Result.IsSuccess)
                        {
                            System.Windows.MessageBox.Show("در فرایند ثبت طرف حساب از طرف سرور مشکلی پیش آمده ");
                        }

                        if (result.Result.IsSuccess)
                        {
                            this.Close();
                        }
                    }
                }
                else
                {
                    var result = sdk.AddClient(token, dto);
                    if (!result.Result.IsSuccess)
                    {
                        System.Windows.MessageBox.Show("در فرایند ثبت طرف حساب از طرف سرور مشکلی پیش آمده ");
                    }

                    if (result.Result.IsSuccess)
                    {
                        this.Close();
                    }
                }
                    
                
               
            }
            else
            {
                dto.User_ID = ClientId;
                var result = sdk.EditeClient(token, dto);
                if (!result.Result.IsSuccess)
                {
                    System.Windows.MessageBox.Show(result.Result.Message);
                }

                if (result.Result.IsSuccess)
                {
                    this.Close();
                }
            }
                
        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (id!=0)
            {
                var user = sdk.GetClientById(token, id);
                if (user.Result.IsSuccess)
                {
                    txtName.Text = user.Result.Data.ClientName;
                    txtAddress.Text = user.Result.Data.ClientAddress;
                    txtPhone.Text = user.Result.Data.ClientPhone;
                    txtClientPartner.Text = user.Result.Data.ClientPartnerName;
                    txtDiscountPercent.Value = (double)user.Result.Data.DiscountPercent;
                    txtMaxCreditValue.Value = user.Result.Data.MaxCreditValue;
                    txtCodeMeli.Text = user.Result.Data.CodeMeli;
                    txtCodeEgtesadi.Text = user.Result.Data.CodeEgtesadi;
                    ClientId = user.Result.Data.User_ID;
                }
            }
        }
    }
}
