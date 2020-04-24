using DoormatSite.Tools;
using Microsoft.AspNetCore.Http;
using PhoenixFutureApiSdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using Telerik.Windows.Media.Imaging.FormatProviders;
using Utility.Tools;

namespace WpfApp1.Pages.Factor
{
    /// <summary>
    /// Interaction logic for ImageEdite.xaml
    /// </summary>
    public partial class ImageEdite
    {
        public string Filename { get; set; }
        public string ImageURL { get; set; }
        private string _token;
        public int _attachid { get; set; }
        public int _factorid { get; set; }

        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public ImageEdite(string token,int attachid,int factorid,string imagpath)
        {
            _token = token;
            _attachid = attachid;
            ImageURL = imagpath;
            _factorid = factorid;
            InitializeComponent();
            AddImageInEditor();
        }
        private void ExportImage()
        {
            IImageFormatProvider formatProvider = ImageFormatProviderManager.GetFormatProviderByExtension(".png");
            bool exists = System.IO.File.Exists(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                         + "\\Temp\\" + Filename+".png");
            if (exists)
                System.IO.File.Delete(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                         + "\\Temp\\" + Filename + ".png");
            using (Stream stream = File.OpenWrite(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\Temp\\"+ Filename + ".png"))
            {
                formatProvider.Export(this.imageEditor.ImageEditor.Image, stream);
            }
            var respons = sdk.DeleteAcctachFile(_token, Convert.ToInt32(_attachid));
            if (respons.Result.IsSuccess)
            {
                var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                         + "\\Temp\\" + Filename + ".png";
                var physicalFile = new FileInfo(path);
                IFormFile formFile = IfromFile.AsMockIFormFile(physicalFile, path);

                var rspons = sdk.AddFactorAttachment(_token, _factorid, formFile, path);
                if (respons.Result.IsSuccess)
                {
                    System.IO.File.Delete(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                         + "\\Temp\\" + Filename + ".png");
                    System.IO.File.Delete(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                                 + "\\Temp\\" + System.IO.Path.GetFileName(ImageURL).ToLower());
                    this.Close();
                }
            }
            
        }
        private void AddImageInEditor()
        {
            var filepath = ImageURL;
            string FileName = System.IO.Path.GetFileName(filepath).ToLower();
            Filename = System.IO.Path.GetFileNameWithoutExtension(filepath);
            using (WebClient wc = new WebClient())
            {
                var durectory = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                         + "\\Temp\\" + FileName;

                wc.DownloadFile(filepath, durectory);
                var fdfd = System.IO.File.OpenRead(durectory);
                Stream stream = fdfd;
                this.imageEditor.Image = new Telerik.Windows.Media.Imaging.RadBitmap(stream);
            }
        }
        private void btnsave_Click(object sender, RoutedEventArgs e)
        {
            ExportImage();
        }

        private void RadWindow_Closed(object sender, WindowClosedEventArgs e)
        {
            if (System.IO.File.Exists(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                         + "\\Temp\\" + Filename + ".png"))
            {
                System.IO.File.Delete(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                         + "\\Temp\\" + Filename + ".png");
            }
            if (System.IO.File.Exists(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                         + "\\Temp\\" + System.IO.Path.GetFileName(ImageURL).ToLower()))
            {
                System.IO.File.Delete(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                         + "\\Temp\\" + System.IO.Path.GetFileName(ImageURL).ToLower());
            }
            
        }
    }
}
