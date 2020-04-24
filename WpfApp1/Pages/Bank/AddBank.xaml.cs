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

namespace WpfApp1.Pages.Bank
{
    /// <summary>
    /// Interaction logic for AddBank.xaml
    /// </summary>
    /// 

    public partial class AddBank
    {
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public string _token;
        public int id = 0;
        private int hid = 0;
        public AddBank()
        {
            InitializeComponent();
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (id != 0)
            {
                var bank = sdk.GetBankById(_token, id);
                txttitle.Text = bank.Result.Data.BankTitle;
                hid = bank.Result.Data.AccountingHeading_ID;
            }
        }

        private void btnok_Click(object sender, RoutedEventArgs e)
        {
            BankDto dto = new BankDto();
            dto.id = id;
            dto.BankTitle = txttitle.Text;
            dto.AccountingHeading_ID = hid;

            if (sdk.GetAllBanks(_token).Result.Data.Where(p => p.BankTitle == dto.BankTitle).Any())
            {
                var s = System.Windows.Forms.MessageBox.Show("بانک اضافه شده تکراری می باشد. آیا از ثبت آن اطمینان دارید؟", "اخطار", MessageBoxButtons.OKCancel);
                if (s == System.Windows.Forms.DialogResult.OK)
                {
                    if (id == 0)
                    {
                        var result = sdk.AddBank(_token, dto);
                        if (!result.Result.IsSuccess)
                        {
                            System.Windows.MessageBox.Show("در فرایند ثبت بانک  از طرف سرور مشکلی پیش آمده ");
                        }

                        if (result.Result.IsSuccess)
                        {
                            this.Close();
                        }

                    }
                    else
                    {
                        var result = sdk.EditeBank(_token, dto);
                        if (!result.Result.IsSuccess)
                        {
                            System.Windows.MessageBox.Show("در فرایند ویرایش بانک  از طرف سرور مشکلی پیش آمده ");
                        }

                        if (result.Result.IsSuccess)
                        {
                            this.Close();
                        }
                    }
                    
                }
            }
            else
            {
                if (id == 0)
                {
                    var result = sdk.AddBank(_token, dto);
                    if (!result.Result.IsSuccess)
                    {
                        System.Windows.MessageBox.Show("در فرایند ثبت بانک  از طرف سرور مشکلی پیش آمده ");
                    }

                    if (result.Result.IsSuccess)
                    {
                        this.Close();
                    }

                }
                else
                {
                    var result = sdk.EditeBank(_token, dto);
                    if (!result.Result.IsSuccess)
                    {
                        System.Windows.MessageBox.Show("در فرایند ویرایش بانک  از طرف سرور مشکلی پیش آمده ");
                    }

                    if (result.Result.IsSuccess)
                    {
                        this.Close();
                    }
                }
            }
        }


        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
