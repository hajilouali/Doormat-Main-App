using DataLayer.Api.Request;
using DataLayer.Api.Response;
using DoormatSite.Tools;
using Fizzler;
using PersianDate;
using PhoenixFutureApiSdk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Telerik.Windows.Controls.ConversationalUI;

namespace Doormat.Pages.TiketManagement
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat
    {
        private Author currentAuthor;
        private Author dormatAuthor;
        private Author otherAuthor;
        private string _token;
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        private TiketDto _selectetTiket { get; set; }
        private List<TextMessage> texts = new List<TextMessage>();
        private List<ProductCardMessage> productCards = new List<ProductCardMessage>();
        private DateTime dattcurent = DateTime.Now;

        public Chat(string Token,TiketDto dto)
        {
            _token = Token;
            _selectetTiket = dto;
            InitializeComponent();
            BindTiketToTiketContent();
            xml.SavetoXml("selecttiketid", _selectetTiket.id.ToString());
            if (this.chat.ToolBarCommands.Count == 0)
            {
                this.chat.ToolBarCommands.Add(new ToolBarCommand() { Text = "ارسال پیوست", Command = new DelegateCommand(OnClickCommandExecute) });

            }
        }

        private void RadChat_SendMessage(object sender, SendMessageEventArgs e)
        {

            // We will handle the event in order to add a new message manually 
            e.Handled = true;
            AddTiketContnt dto = new AddTiketContnt()
            {
                Text = (e.Message as TextMessage).Text,
                File = null,
            };
            
            var result = sdk.AddTiketContent(_selectetTiket.id, dto, _token);
            if (result.IsCompleted)
            {
                this.chat.CurrentAuthor = currentAuthor;

                var updatedMessageText = (e.Message as TextMessage).Text;
                var textMessage = new TextMessage(this.currentAuthor, updatedMessageText, "ارسال", DateTime.Now);
                textMessage.InlineViewModel.StatusVisibility = Visibility.Visible;
                this.chat.AddMessage(textMessage);
                this.texts.Add(textMessage);
                //this.chat.AddMessage(this.chat.CurrentAuthor, updatedMessageText);
            }


        }
        private void OnClickCommandExecute(object obj)
        {
            AddAttachment dto = new AddAttachment(_token, Convert.ToInt32(xml.loadline("selecttiketid")));
            dto.Owner = this;
            dto.ShowDialog();
            if (dto.model != null)
            {

                BindTiketToTiketContent();
            }

        }
        private void chat_ReportMessageResult(object sender, MessageResultEventArgs e)
        {
            WebClient webClient = new WebClient();
            string url = string.Format(xml.loadline("serverApi") + "/uploads/Tiket/" + _selectetTiket.id + "/" + (e.Message as ProductCardMessage).SubTitle);
            string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\Temp\\Tikets\\" + (e.Message as ProductCardMessage).SubTitle;
            webClient.DownloadFile(url, path);



            string pathour = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\Temp\\Tikets\\";
            Process.Start(pathour);

        }

        private void BindTiketToTiketContent()
        {
            foreach (var item in this.texts)
            {
                this.chat.RemoveMessage(item);
            }
            this.texts.Clear();
            foreach (var item in productCards)
            {
                this.chat.RemoveMessage(item);
            }
            this.productCards.Clear();
            var tiketcontent = sdk.GetTiketContent(_selectetTiket.id, _token).Result.Data;
            //this.chat.AutoIncludeTimeBreaks = new RadChat();
            currentAuthor = new Author(sdk.GetUserBio(_token).Result.Data.FullName);
            otherAuthor = new Author(tiketcontent.user);

            if (tiketcontent.DataCreate.Date.ToShortDateString() != dattcurent.ToShortDateString())
            {
                dattcurent = tiketcontent.DataCreate;
                this.chat.AddTimeBreak(dattcurent.ToFa("D"));
            }

            foreach (var item in tiketcontent.tiketContents)
            {
                if (item.dataModified.Date.ToShortDateString() != dattcurent.Date.ToShortDateString())
                {
                    dattcurent = item.dataCreate.Date;
                    this.chat.AddTimeBreak(dattcurent.ToFa("D"));
                }

                if (item.isAdminSide)
                {

                    dormatAuthor = new Author(item.user);
                    this.chat.CurrentAuthor = dormatAuthor;
                    if (!string.IsNullOrEmpty(item.text))
                    {
                        var textMessage = new TextMessage(this.dormatAuthor, item.text, "ارسال", item.dataCreate);
                        textMessage.InlineViewModel.StatusVisibility = Visibility.Visible;
                        this.chat.AddMessage(textMessage);
                        this.texts.Add(textMessage);
                    }
                    if (!string.IsNullOrEmpty(item.fileURL))
                    {
                        ProductCardMessage productCardMessage = new ProductCardMessage(dormatAuthor);
                        var title = "فایل پیوست ";
                        productCardMessage.Title = title;
                        productCardMessage.SubTitle = item.fileURL;
                        if (IsImage(item.fileURL))
                        {
                            string url = string.Format(xml.loadline("serverApi") + "/uploads/Tiket/" + _selectetTiket.id + "/" + item.fileURL);
                            productCardMessage.ImageSource = new BitmapImage(new Uri(url));

                        }
                        productCardMessage.RatingItemsCount = 0;
                        productCardMessage.ActionResultsOrientation = Orientation.Horizontal;
                        productCardMessage.ReportActions.Add(new ValueResponseAction(productCardMessage) { Text = "مشاهده پیوست", TextResultValue = "Order", DataObjectValue = title });
                        this.chat.AddMessage(productCardMessage);
                        this.productCards.Add(productCardMessage);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(item.text))
                    {
                        var textMessage = new TextMessage(this.otherAuthor, item.text, "ارسال", item.dataCreate);
                        textMessage.InlineViewModel.StatusVisibility = Visibility.Visible;
                        this.chat.AddMessage(textMessage);
                        this.texts.Add(textMessage);
                    }
                    if (!string.IsNullOrEmpty(item.fileURL))
                    {
                        ProductCardMessage productCardMessage = new ProductCardMessage(otherAuthor);
                        var title = "فایل پیوست ";
                        productCardMessage.Title = title;
                        productCardMessage.SubTitle = item.fileURL;
                        if (IsImage(item.fileURL))
                        {
                            string url = string.Format(xml.loadline("serverApi") + "/uploads/Tiket/" + _selectetTiket.id + "/" + item.fileURL);

                            productCardMessage.ImageSource = new BitmapImage(new Uri(url));

                        }
                        productCardMessage.RatingItemsCount = 0;
                        productCardMessage.ActionResultsOrientation = Orientation.Horizontal;
                        productCardMessage.ReportActions.Add(new ValueResponseAction(productCardMessage) { Text = "مشاهده پیوست", TextResultValue = "Order", DataObjectValue = title });
                        this.chat.AddMessage(productCardMessage);
                        this.productCards.Add(productCardMessage);
                    }
                }
                this.chat.CurrentAuthor = currentAuthor;
                this.chat.AutoIncludeTimeBreaks = false;
            }

        }
        private bool IsImage(string filename)
        {
            var ext = System.IO.Path.GetExtension(filename);
            string[] extensions = new string[] { ".jpg", ".jpeg", ".png" };
            foreach (var item in extensions)
            {
                if (ext == item)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
