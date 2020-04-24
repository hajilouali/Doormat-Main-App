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

namespace WpfApp1.Pages.Factor
{
    /// <summary>
    /// Interaction logic for FactorAttachment.xaml
    /// </summary>
    public partial class FactorAttachment
    {
        private string _token;
        public int _FactorId { get; set; }
        private List<DataLayer.Api.Response.FactorAttachment> ListFactorattachment { get; set; }

        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public FactorAttachment(string token,int id)
        {
            _token = token;
            _FactorId = id;
            InitializeComponent();
            itemsurce();
        }

        private void listboxImages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                thumlineImage.Source = (listboxImages.SelectedItem as Image).Source;
            }
            catch (Exception)
            {

                
            }
          
            
        }
        private void itemsurce()
        {
            var attachs = sdk.GetFactorAttachment(_token, _FactorId);
            List<Image> list = new List<Image>();
            if (attachs.Result.IsSuccess)
            {
                ListFactorattachment = attachs.Result.Data.ToList();
                foreach (var item in attachs.Result.Data)
                {
                    Image data = new Image();
                    data.Width = 50;
                    data.Uid = item.id.ToString();
                    data.Source = new BitmapImage(new Uri(string.Format(xml.loadline("serverApi") + "/uploads/Factors/" + _FactorId + "/" + item.FileName)));
                    list.Add(data);
                }
                listboxImages.ItemsSource = list;
            }
            if (list.Count>0)
            {
                thumlineImage.Source = list.FirstOrDefault().Source;
            }
            
        }

        private void btnnew_Click(object sender, RoutedEventArgs e)
        {
            UploadAttachment upload = new UploadAttachment(_token, _FactorId);
            upload.Owner = this;
            upload.ShowDialog();
        }

        private void RadWindow_Activated(object sender, EventArgs e)
        {
            itemsurce();
        }

        private void btnRemoveAttach_Click(object sender, RoutedEventArgs e)
        {
            var id = (listboxImages.SelectedItem as Image).Uid;
            var mess = FarsiMessageBox.MessageBox.Show("سوال", "آیا از حذف فایل پیوست اطمینان دارید ؟", FarsiMessageBox.MessageBox.Buttons.YesNo, FarsiMessageBox.MessageBox.Icons.Warning);
            if (mess== System.Windows.Forms.DialogResult.Yes)
            {
                var respons = sdk.DeleteAcctachFile(_token, Convert.ToInt32(id));
                if (!respons.Result.IsSuccess)
                {
                    FarsiMessageBox.MessageBox.Show("خطا", "در فرایند حذف فایل پیوست خطایی پیش آمده", FarsiMessageBox.MessageBox.Buttons.OK, FarsiMessageBox.MessageBox.Icons.Error);

                }
            }
            
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            var imafe = thumlineImage.Source;
            var bi = new BitmapImage();
            var tb = new BitmapImage();
            bi.BeginInit();
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.UriSource = new Uri(imafe.ToString());
            
            bi.EndInit();
            if (bi.Width > bi.Height)
            {
                
                tb.BeginInit();
                tb.UriSource = bi.UriSource;
                // Set image rotation.
                
                tb.Rotation = Rotation.Rotate90;
                tb.EndInit();
            }
            var vis = new DrawingVisual();
            using (var dc = vis.RenderOpen())
            {
                if (bi.Width > bi.Height)
                {
                    dc.DrawImage(tb, new Rect(0, 0, 781, 1105));

                }
                else
                {
                    dc.DrawImage(bi, new Rect(0, 0, 781, 1105));
                }
                
            }

            var pdialog = new PrintDialog();
            if (pdialog.ShowDialog() == true)
            {
                pdialog.PrintVisual(vis, "My Image");
            }
        }

        private void btnedit_Click(object sender, RoutedEventArgs e)
        {
            
            var path = thumlineImage.Source.ToString();
            var id = ListFactorattachment.Where(p=>p.FileName== System.IO.Path.GetFileName(path)).SingleOrDefault();
            ImageEdite image = new ImageEdite(_token, id.id, id.Facor_ID, path );
            image.Owner = this;
            image.ShowDialog();
        }
    }
}
