using DoormatSite.Tools;
using Microsoft.AspNetCore.Http;
using Moq;
using PhoenixFutureApiSdk;
using System;
using System.Collections.Generic;
using System.IO;
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
using Utility.Tools;

namespace WpfApp1.Pages.Factor
{
    /// <summary>
    /// Interaction logic for UploadAttachment.xaml
    /// </summary>
    public partial class UploadAttachment
    {
        private string _token;
        public int _FactorId { get; set; }

        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public UploadAttachment(string token, int id)
        {
            _token = token;
            _FactorId = id;
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            var path = filepiker.FilePath;
            var physicalFile = new FileInfo(path);
            IFormFile formFile = IfromFile.AsMockIFormFile(physicalFile,path);
            
                 var rspons = sdk.AddFactorAttachment(_token, _FactorId, formFile, path);
                if (rspons.Result.IsSuccess)
                {
                    this.Close();
                }
                else
                {
                    FarsiMessageBox.MessageBox.Show("خطا", "در فرایند ثبت فایل پیوست خطایی پیش آمده", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Error);
                }
            
            
        }
        
    }
}
