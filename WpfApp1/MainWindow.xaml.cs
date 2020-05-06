using BindComboboxToEnum;
using DataLayer;
using DataLayer.Api.Request;
using DataLayer.Api.Response;
using Doormat.Pages.TiketManagement;
using DoormatSite.Tools;
using MD.PersianDateTime;
using Moq;
using PersianDate;
using PhoenixFutureApiSdk;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TaskBoard;
using WpfApp1.Pages;
using WpfApp1.Pages.Bank;
using WpfApp1.Pages.Clients;
using WpfApp1.Pages.Experts;
using WpfApp1.Pages.Factor;
using WpfApp1.Pages.Manufacture;
using WpfApp1.Pages.Products;
using WpfApp1.Pages.Sanad;
using WpfApp1.Pages.Users;
using WpfApp1.RepostFormTools;
using WpfApp1.RepostFormTools.ViewModel;
using WpfApp1.Reposts.Accounting;
using WpfApp1.Reposts.Factor;
using FactorAttachment = WpfApp1.Pages.Factor.FactorAttachment;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string token;
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public MainWindow(string _token)
        {
            token = _token;
            InitializeComponent();
            var user = sdk.GetUserBio(token).Result;
            DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                PersianDateTime persianDateTime = new PersianDateTime(DateTime.Now);
                //this.LblStatus.Content = DateTime.Now.ToString("ddd, dd MMM yyy - HH':'mm':'ss ");
                this.LblStatus.Content = persianDateTime.ToLongDateTimeString();
                
            }, this.Dispatcher);
            GetTasks();
            DispatcherTimer timerr = new DispatcherTimer(new TimeSpan(0, 0, 60), DispatcherPriority.Normal, delegate
            {
                //this.LblStatus.Content = DateTime.Now.ToString("ddd, dd MMM yyy - HH':'mm':'ss ");
                if (user.Data.RollName.Where(p => p.Contains("Admin") || p.Contains("Accountants")).Any())
                {
                    
                }
            }, this.Dispatcher);

            #region MyRegion


            #endregion

        }


        public void GetTasks()
        {
            ObservableCollection<TaskBoardCardModel> tasks = new ObservableCollection<TaskBoardCardModel>();
            var dto = new GetFactor()
            {
                ISAllType = false,
                StartDate = "",
                EndDate = "",
                ClientID = 0,
                FactorType = DataLayer.Api.Response.FactorType.PendingToAccept,
            };
            var pendingfactor = sdk.GetAllFactor(token, dto);
            if (pendingfactor.IsCompleted)
            {
                if (pendingfactor.Result.Data.Any())
                {
                    foreach (var item in pendingfactor.Result.Data)
                    {
                        tasks.Add(
                        new TaskBoardCardModel()
                        {
                            Assignee = item.User_Name,
                            Title = string.Format("شماره فاکتور:" + item.id),
                            Description = item.User_Name,
                            State = "فاکتور های در انتظار تایید",
                            CategoryName = "High"
                        });
                        var alert = new RadDesktopAlert();
                        alert.Header = string.Format("شماره فاکتور:" + item.id);
                        alert.Content = item.User_Name;
                        alert.ShowDuration = 3000;

                        RadDesktopAlertManager manager = new RadDesktopAlertManager();
                        manager.ShowAlert(alert);
                    }
                }
                rlblpendingfactorcount.Content = pendingfactor.Result.Data.Count;
            }
            var gss = new GetFactor()
            {
                ClientID = 0,
                ISAllType = true,
                FactorType = 0,
                StartDate = DateTime.Now.ToFa(),
                EndDate = DateTime.Now.ToFa()
            };
            var factors = sdk.GetAllFactor(token, gss);
            if (factors.IsCompleted)
            {
                if (factors.Result.Data.Any())
                {
                    foreach (var item in factors.Result.Data.Where(p => p.FactorType == FactorType.PishFactor))
                    {
                        tasks.Add(
                        new TaskBoardCardModel()
                        {
                            Assignee = item.User_Name,
                            Title = string.Format("شماره فاکتور:" + item.id),
                            Description = item.User_Name,
                            State = "پیش فاکتور ها",
                            CategoryName = "Medium"
                        });
                    }
                    foreach (var item in factors.Result.Data.Where(p=>p.FactorType==FactorType.Factor))
                    {
                        tasks.Add(
                        new TaskBoardCardModel()
                        {
                            Assignee = item.User_Name,
                            Title = string.Format("شماره فاکتور:"+item.id),
                            Description = item.User_Name,
                            State = "فاکتور ها",
                            CategoryName = "Low"
                        });
                    }
                }
                rlblfactorscount.Content = factors.Result.Data.Count();

                rlblfactorcount.Content = factors.Result.Data.Where(p => p.FactorType == FactorType.Factor).Count();
                rlblpishfactorcount.Content = factors.Result.Data.Where(p => p.FactorType == FactorType.PishFactor).Count();

            }


            var tiketpending  = sdk.GetTikets(token);            
            if (tiketpending.IsCompleted)
            {
                if (tiketpending.Result.Data.Any())
                {
                    foreach (var item in tiketpending.Result.Data)
                    {
                        tasks.Add(
                        new TaskBoardCardModel()
                        {
                            Assignee = item.user,
                            Title = item.user,
                            Description = item.Title,
                            State = "تیکت های در انتظار تایید",
                            CategoryName = "High"
                        });
                        var alert = new RadDesktopAlert();
                        alert.Header = item.Title;
                        alert.Content =item.tiketContents.FirstOrDefault().tiketId;
                        alert.ShowDuration = 3000;

                        RadDesktopAlertManager manager = new RadDesktopAlertManager();
                        manager.ShowAlert(alert);
                    }
                }
                rlbltiketcount.Content = tiketpending.Result.Data.Count;
            }
            var bazdid = sdk.GetExperts(token);
            if (bazdid.IsCompleted)
            {
                if (bazdid.Result.Data.Any())
                {
                    foreach (var item in bazdid.Result.Data)
                    {
                        tasks.Add(
                        new TaskBoardCardModel()
                        {
                            Assignee = item.ClientName,
                            Title = item.ClientName,
                            Description = item.Clientaddress,
                            State = "در انتظار بازدید",
                            CategoryName = "Medium"
                        });
                    }
                }
                rlblexpertcount.Content = bazdid.Result.Data.Count;
            }
            var manufactures = sdk.GetGetManufacture(token);
            if (manufactures.IsCompleted)
            {
                if (manufactures.Result.Data.Any())
                {
                    foreach (var item in manufactures.Result.Data)
                    {
                        tasks.Add(
                       new TaskBoardCardModel()
                       {
                           Assignee = item.FactorClientName,
                           Title = string.Format("شماره فاکتور :"+ item.Factor_ID),
                           Description = string.Format("آخرین وضعیت :"+ item.ConditionManufactureTitle),
                           State = "سفارشات در تولید",
                           CategoryName = "Low"
                       });
                    }
                }
                rlblmanufaccount.Content=manufactures.Result.Data.Count;
            }
            taskBoard.ItemsSource = tasks;
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataApi d = new DataApi();
            FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));

            var dateo = d.GetDateOccasion();
            if (dateo.Result.type == DataLayer.Api.typeDateRespuns.success)
            {
                if (dateo.Result.values.Count != 0)
                {
                    Lbldateoc.Content = "مناسبت های روز :";
                    foreach (var item in dateo.Result.values)
                    {
                        Lbldateoc.Content += item.occasion + ",";
                    }
                }
            }
            var user = sdk.GetUserBio(token).Result;
            lblusername.Content = " کاربر :" + user.Data.FullName;
            #region Rollmanager
            toolbaraccounting.Visibility = Visibility.Collapsed;
            toolbarexpert.Visibility = Visibility.Collapsed;
            toolbarmanufactore.Visibility = Visibility.Collapsed;
            menuaccounting.Visibility = Visibility.Collapsed;
            menuediting.Visibility = Visibility.Collapsed;
            expertmenu.Visibility = Visibility.Collapsed;
            menureports.Visibility = Visibility.Collapsed;
            menumanufactore.Visibility = Visibility.Collapsed;
            settingmenu.Visibility = Visibility.Collapsed;
            eventsss.Visibility = Visibility.Collapsed;
            taskBoard.Visibility = Visibility.Collapsed;
            Tikets.Visibility = Visibility.Collapsed;


            if (user.Data.RollName.Where(p => p.Equals("Admin")).Any())
            {
                toolbaraccounting.Visibility = Visibility.Visible;
                toolbarexpert.Visibility = Visibility.Visible;
                toolbarmanufactore.Visibility = Visibility.Visible;
                menuaccounting.Visibility = Visibility.Visible;
                menuediting.Visibility = Visibility.Visible;
                expertmenu.Visibility = Visibility.Visible;
                menureports.Visibility = Visibility.Visible;
                menumanufactore.Visibility = Visibility.Visible;
                settingmenu.Visibility = Visibility.Visible;
                eventsss.Visibility = Visibility.Visible;
                taskBoard.Visibility = Visibility.Visible;
                Tikets.Visibility = Visibility.Visible;

            }
            if (user.Data.RollName.Where(p => p.Equals("Accountants")).Any())
            {
                toolbaraccounting.Visibility = Visibility.Visible;
                toolbarexpert.Visibility = Visibility.Visible;
                toolbarmanufactore.Visibility = Visibility.Visible;
                menuaccounting.Visibility = Visibility.Visible;
                menuediting.Visibility = Visibility.Visible;
                expertmenu.Visibility = Visibility.Visible;
                menureports.Visibility = Visibility.Visible;
                menumanufactore.Visibility = Visibility.Visible;
                eventsss.Visibility = Visibility.Visible;
                taskBoard.Visibility = Visibility.Visible;
                Tikets.Visibility = Visibility.Visible;

            }
            if (user.Data.RollName.Where(p => p.Equals("Manufacturers")).Any())
            {
                menumanufactore.Visibility = Visibility.Visible;
                toolbarmanufactore.Visibility = Visibility.Visible;
                Tikets.Visibility = Visibility.Visible;


            }
            if (user.Data.RollName.Where(p => p.Equals("Experts")).Any())
            {
                expertmenu.Visibility = Visibility.Visible;
                toolbarexpert.Visibility = Visibility.Visible;


            }
            if (user.Data.RollName.Where(p => p.Equals("CompanyPartner")).Any())
            {
            }
            if (user.Data.RollName.Where(p => p.Equals("CellPartner")).Any())
            {
            }
            #endregion






        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductMain productMain = new ProductMain(token);
            productMain.Owner = this;
            productMain.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clients productMain = new Clients(token);
            productMain.Owner = this;
            productMain.Show();
        }

        private void btnBank_Click(object sender, RoutedEventArgs e)
        {
            Bank bank = new Bank(token);
            bank.Owner = this;
            bank.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            bank.Show();
        }

        private void btnFactor_Click(object sender, RoutedEventArgs e)
        {
            Factor factor = new Factor(token, 0);
            factor.Owner = this;
            factor.Show();
        }

        private void FactorView_Click(object sender, RoutedEventArgs e)
        {
            int factorid = 0;
            FactorDto factor = null;
            Factorview factorview = new Factorview();
            factorview.Owner = this;
            if (factorview.ShowDialog() == true)
            {
                factorid = factorview.FactorId;
            }
            if (factorid != 0)
            {
                var response = sdk.GetFactorById(token, factorid);
                if (response.Result.IsSuccess)
                {
                    factor = response.Result.Data;

                    DataTable dataTable = new DataTable();

                    dataTable.Columns.Add("RowNumber");
                    dataTable.Columns.Add("ProductName");
                    dataTable.Columns.Add("Width");
                    dataTable.Columns.Add("length");
                    dataTable.Columns.Add("Unit");
                    dataTable.Columns.Add("Metraj");
                    dataTable.Columns.Add("Price");
                    dataTable.Columns.Add("RowDiscription");
                    dataTable.Columns.Add("UnitPrice");
                    int inss = 1;
                    foreach (var item in factor.Product_Factor)
                    {

                        dataTable.Rows.Add(inss, item.ProductAndService, item.Width, item.length, item.Unit, item.Metraj, item.Price, item.RowDiscription, item.UnitPrice);
                        inss++;
                    }
                    var cli = sdk.GetClientById(token, factor.Client_ID).Result.Data;
                    DataTable Factorinfo = new DataTable();
                    Factorinfo.Columns.Add("client_ID");
                    Factorinfo.Columns.Add("user_ID");
                    Factorinfo.Columns.Add("discount");
                    Factorinfo.Columns.Add("discription");
                    Factorinfo.Columns.Add("ClientName");
                    Factorinfo.Columns.Add("UserName");
                    Factorinfo.Columns.Add("dateTime");
                    Factorinfo.Columns.Add("totalPrice");
                    Factorinfo.Columns.Add("taxes");
                    Factorinfo.Columns.Add("factorPrice");
                    Factorinfo.Columns.Add("factorCodeView");
                    Factorinfo.Columns.Add("factorType");
                    Factorinfo.Columns.Add("id");
                    Factorinfo.Columns.Add("ClientAddress");
                    Factorinfo.Columns.Add("ClientPhone");
                    Factorinfo.Rows.Add(factor.Client_ID, factor.User_ID, factor.Discount, factor.Discription, cli.ClientName,
                       sdk.GetUserBio(token).Result.Data.FullName, factor.DateTime, factor.TotalPrice, factor.Taxes, factor.FactorPrice, factor.FactorCodeView,
                       factor.FactorType == FactorType.Factor ? "فاکتور فروش" : "پیش فاکتور", factor.id, cli.ClientAddress, cli.ClientPhone);
                    StiReport stiReport = new StiReport();
                    stiReport.RegData("DT", dataTable);
                    stiReport.RegData("FactorInfo", Factorinfo);
                    string path = "";
                    if (factor.Taxes != 0)
                    {
                        path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                               + "\\ReportsForm\\RasmiFactor.mrt";
                    }
                    else
                    {
                        path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                               + "\\ReportsForm\\Factor.mrt";
                    }

                    stiReport.Load(path);
                    stiReport.Show();



                }
                else
                {
                    MessageBox.Show("فاکتور مورد نظر یافت نشد");
                    goto ento;
                }
            }
        ento: int ddd = 0;

        }

        private void FactorEdite_Click(object sender, RoutedEventArgs e)
        {
            Factorview factorview = new Factorview();
            factorview.Header = "ویرایش فاکتور";
            factorview.Owner = this;
            int factorid = 0;
            if (factorview.ShowDialog() == true)
            {
                factorid = factorview.FactorId;
            }
            Factor factor = new Factor(token, factorid);

            factor.Owner = this;
            factor.Header = "ویرایش فاکتور";
            factor.ShowDialog();
        }

        private void removefactor_Click(object sender, RoutedEventArgs e)
        {

            Factorview factorview = new Factorview();
            factorview.Header = "حذف فاکتور";
            factorview.Owner = this;
            int factorid = 0;
            if (factorview.ShowDialog() == true)
            {
                factorid = factorview.FactorId;
            }
            if (factorid != 0)
            {
                var factor = sdk.GetFactorById(token, factorid);
                if (factor.Result.IsSuccess)
                {
                    if (MessageBox.Show(string.Format("آیا از پاک کردن فاکتور {0} اطمینان دارید؟", factorid), "اخطار", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        var result = sdk.deleteFactor(token, factorid);
                        if (result.Result.IsSuccess)
                        {
                            MessageBox.Show("فاکتور مورد نظر با موفقیت حذف کردید", "", MessageBoxButton.OK);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("فاکتور مورد نظر یافت نشد", "", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("فاکتور مورد نظر یافت نشد", "", MessageBoxButton.OK);
            }

        }

        private void changetofactor_Click(object sender, RoutedEventArgs e)
        {

            Factorview factorview = new Factorview();
            factorview.Header = "تبدیل به فاکتور";
            factorview.Owner = this;
            int factorid = 0;
            if (factorview.ShowDialog() == true)
            {
                factorid = factorview.FactorId;
            }
            if (factorid != 0)
            {
                var factor = sdk.GetFactorById(token, factorid);
                if (factor.Result.IsSuccess)
                {
                    if (factor.Result.Data.FactorType == FactorType.PishFactor)
                    {
                        if (MessageBox.Show(string.Format("آیا از تبدیل به فاکتور شماره {0} اطمینان دارید؟", factorid), "اخطار", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            var result = sdk.CheangToFactor(token, factorid);
                            if (result.Result.IsSuccess)
                            {
                                FactorReports.FactorViewReport(result.Result.Data, sdk.GetClientById(token, result.Result.Data.Client_ID).Result.Data);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("شماره فاکتور ذکر شده از نوع فاکتور می باشد.", "", MessageBoxButton.OK);
                    }
                }
                else
                {
                    MessageBox.Show("فاکتور مورد نظر یافت نشد", "", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("فاکتور مورد نظر یافت نشد", "", MessageBoxButton.OK);
            }

        }

        private void changetopishfactor_Click(object sender, RoutedEventArgs e)
        {

            Factorview factorview = new Factorview();
            factorview.Header = "تبدیل به پیش فاکتور";
            factorview.Owner = this;
            int factorid = 0;
            if (factorview.ShowDialog() == true)
            {
                factorid = factorview.FactorId;
            }
            if (factorid != 0)
            {
                var factor = sdk.GetFactorById(token, factorid);
                if (factor.Result.IsSuccess)
                {
                    if (factor.Result.Data.FactorType == FactorType.Factor)
                    {
                        if (MessageBox.Show(string.Format("آیا از تبدیل به پیش فاکتور شماره {0} اطمینان دارید؟", factorid), "اخطار", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            var result = sdk.CheangToPishFactor(token, factorid);
                            if (result.Result.IsSuccess)
                            {
                                FactorReports.FactorViewReport(result.Result.Data, sdk.GetClientById(token, result.Result.Data.Client_ID).Result.Data);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("شماره فاکتور ذکر شده از نوع  پیش فاکتور می باشد.", "", MessageBoxButton.OK);
                    }
                }
                else
                {
                    MessageBox.Show("فاکتور مورد نظر یافت نشد", "", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("فاکتور مورد نظر یافت نشد", "", MessageBoxButton.OK);
            }

        }

        private void btnSanad_Click(object sender, RoutedEventArgs e)
        {
            AddSanad addSanad = new AddSanad(token);
            addSanad.Header = "ثبت سند";
            addSanad.Owner = this;
            addSanad.ShowDialog();
        }

        private void SanadEdit_Click(object sender, RoutedEventArgs e)
        {
            Factorview factorview = new Factorview();
            factorview.Header = "ویرایش سند";
            factorview.lbltitle.Content = "شماره سند";
            factorview.Owner = this;
            int sanadid = 0;
            if (factorview.ShowDialog() == true)
            {
                sanadid = factorview.FactorId;

            }
            var sanad = sdk.GetSanadById(token, sanadid);
            if (sanad.Result.IsSuccess)
            {
                AddSanad addSanad = new AddSanad(token, sanadid);
                addSanad.Header = "ویرایش سند";
                addSanad.Owner = this;
                addSanad.ShowDialog();
            }
            else
            {
                MessageBox.Show("سند مورد نظر یافت نشد", "", MessageBoxButton.OK);
            }
        }

        private void sanadview_Click(object sender, RoutedEventArgs e)
        {
            Factorview factorview = new Factorview();
            factorview.Header = "ویرایش سند";
            factorview.lbltitle.Content = "شماره سند";
            factorview.Owner = this;
            int sanadid = 0;
            if (factorview.ShowDialog() == true)
            {
                sanadid = factorview.FactorId;

            }
            var sanad = sdk.GetSanadById(token, sanadid);
            if (sanad.Result.IsSuccess)
            {
                List<SanadsReportsViewModel> model = new List<SanadsReportsViewModel>();
                foreach (var item in sanad.Result.Data.Sanads)
                {
                    model.Add(new SanadsReportsViewModel()
                    {
                        AccountingHeading_ID = item.AccountingHeading_ID,
                        AccountingHeading = item.AccountingHeading,
                        Bedehkari = item.Bedehkari,
                        Bestankari = item.Bestankari,
                        Comment = item.Comment,
                        DateTime = item.stringDatatime,
                        Discription = sanad.Result.Data.Discription,
                        Sanad_ID = sanad.Result.Data.id
                    });
                }
                SanadsReports.SanadsView(model);
            }
            else
            {
                MessageBox.Show("سند مورد نظر یافت نشد", "", MessageBoxButton.OK);
            }
        }

        private void appSetting_Click(object sender, RoutedEventArgs e)
        {

            ServerSetting server = new ServerSetting();
            server.Owner = this;
            server.Show();
        }

        private void btnMeinClient_Click(object sender, RoutedEventArgs e)
        {
            MoeinClient moeinClient = new MoeinClient(token);
            moeinClient.ShowDialog();
        }

        private void sanadRemove_Click(object sender, RoutedEventArgs e)
        {
            Factorview factorview = new Factorview();
            factorview.Header = "ویرایش سند";
            factorview.lbltitle.Content = "شماره سند";
            factorview.Owner = this;
            int sanadid = 0;
            if (factorview.ShowDialog() == true)
            {
                sanadid = factorview.FactorId;

            }
            var sanad = sdk.GetSanadById(token, sanadid);
            if (sanad.Result.IsSuccess)
            {
                if (MessageBox.Show(string.Format("آیا از حذف سند شماره {0} اطمینان دارید ؟", sanadid), "اخطار", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = sdk.DeletSanad(token, sanadid);
                    if (result.Result.IsSuccess)
                    {

                    }
                    else
                    {
                        MessageBox.Show("در فرایند حذف سند مورد نظر مشکلی پیش آمده", "", MessageBoxButton.OK);
                    }
                }

            }
            else
            {
                MessageBox.Show("سند مورد نظر یافت نشد", "", MessageBoxButton.OK);
            }
        }

        private void meinAccounting_Click(object sender, RoutedEventArgs e)
        {
            Moein moeinClient = new Moein(token);
            moeinClient.Owner = this;
            moeinClient.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            moeinClient.ShowDialog();
        }

        private void moeinClients_Click(object sender, RoutedEventArgs e)
        {
            MoeinClient moeinClient = new MoeinClient(token);
            moeinClient.Owner = this;
            moeinClient.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            moeinClient.ShowDialog();
        }

        private void btnAccounting_Click(object sender, RoutedEventArgs e)
        {
            Accounting Accounting = new Accounting(token);
            Accounting.Owner = this;
            Accounting.Show();
        }

        private void AccountingClients_Click(object sender, RoutedEventArgs e)
        {
            ClientAccounting client = new ClientAccounting(token);
            client.Owner = this;
            client.ShowDialog();
        }

        private void mrorFactor_Click(object sender, RoutedEventArgs e)
        {
            Factors factors = new Factors(token);
            factors.Owner = this;
            factors.ShowDialog();
        }

        private void btnFactorattachment_Click(object sender, RoutedEventArgs e)
        {
            Factorview factorview = new Factorview();
            factorview.Header = "پیوست های فاکتور";
            factorview.lbltitle.Content = "شماره فاکتور";
            factorview.Owner = this;
            int sanadid = 0;
            if (factorview.ShowDialog() == true)
            {
                sanadid = factorview.FactorId;

            }
            var sanad = sdk.GetFactorById(token, sanadid);
            if (sanad.Result.IsSuccess)
            {
                FactorAttachment factor = new FactorAttachment(token, sanad.Result.Data.id);

                factor.Owner = this;
                factor.Show();
            }
            else
            {
                MessageBox.Show("سند مورد نظر یافت نشد", "", MessageBoxButton.OK);
            }

        }

        private void UserSitting_Click(object sender, RoutedEventArgs e)
        {
            Users users = new Users(token);
            users.Owner = this;
            users.ShowDialog();

        }

        private void btnManufacture_Click(object sender, RoutedEventArgs e)
        {
            MainManufacture main = new MainManufacture(token);
            main.Owner = this;
            main.Show();
        }

        private void manufactureReport_Click(object sender, RoutedEventArgs e)
        {
            ManufacturyReport report = new ManufacturyReport(token);
            report.Owner = this;
            report.Show();
        }

        private void expertmanage_Click(object sender, RoutedEventArgs e)
        {
            ExpertsMain main = new ExpertsMain(token);
            main.Owner = this;
            main.Show();
        }

        private void expert_Click(object sender, RoutedEventArgs e)
        {
            Expert expert = new Expert(token);
            expert.Owner = this;
            expert.Show();
        }

        private void yearshop_Click(object sender, RoutedEventArgs e)
        {
            Factorview factorview = new Factorview();
            factorview.Header = "گزارش نموداری";
            factorview.lbltitle.Content = "سال :";
            factorview.Owner = this;
            int sanadid = 0;
            if (factorview.ShowDialog() == true)
            {
                sanadid = factorview.FactorId;
                CalculateReport report = new CalculateReport(token);
                report.yearshop(sanadid);

            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.token = token;
            product.Title = "افزودن محصول جدید";
            product.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            product.Owner = this;

            product.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient();
            addClient.token = token;
            addClient.Owner = this;
            addClient.Header = "افزودن طرف حساب";
            addClient.ShowDialog();
        }

        private void btnFarcorMror_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFarcorpartner_Click(object sender, RoutedEventArgs e)
        {
            FactorInPending factorIn = new FactorInPending(token);
            factorIn.Owner = this;
            factorIn.Show();
        }

        private void btnfactormror_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTiketsmanagment_Click(object sender, RoutedEventArgs e)
        {
            MainTikets mainTikets = new MainTikets(token);
            mainTikets.Owner = this;
            mainTikets.Show();
        }

        private void btnTiketsoverview_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
