using DataLayer.Api.Response;
using DoormatSite.Tools;
using Microsoft.AspNetCore.Http;
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

namespace Doormat.Pages.TiketManagement
{
    /// <summary>
    /// Interaction logic for AddAttachment.xaml
    /// </summary>
    public partial class AddAttachment
    {
        private string _token;
        public int _TiketId { get; set; }
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public TiketContentDto model;
        public AddAttachment(string token, int id)
        {
            _token = token;
            _TiketId = id;
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            var path = filepiker.FilePath;
            var physicalFile = new FileInfo(path);
            IFormFile formFile = IfromFile.AsMockIFormFile(physicalFile, path);

            var rspons = sdk.AddTiketAttachment(_token, _TiketId, formFile, path);
            if (rspons.Result.IsSuccess)
            {
                this.model = rspons.Result.Data;
                this.Close();
            }
            else
            {
                FarsiMessageBox.MessageBox.Show("خطا", "در فرایند ثبت فایل پیوست خطایی پیش آمده", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Error);
            }

        }
    }
}
