using DataLayer;
using DataLayer.Api.Response;
using DoormatSite.Tools;
using PhoenixFutureApiSdk;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
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
using Telerik.Windows.Controls.GridView;

namespace WpfApp1.Pages.Factor
{
    /// <summary>
    /// Interaction logic for Factor.xaml
    /// </summary>
    public partial class Factor
    {
        private string _token;
        public int factorid = 0;
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public FactorType factorType = FactorType.PishFactor;
        public ObservableCollection<ProductAndServiceDto> products { get; set; }
        public ObservableCollection<FactorAndProduct> Rows { get; set; }

        private ObservableCollection<MenuItem> rowContextMenuItems;
        public decimal price { get; set; }
        private int ClientId = 0;
        private decimal Discount = 0;
        private decimal DiscountPercent = 0;
        private bool IsTaxteable;
        private GridViewHeaderCell ClickedHeader
        {
            get
            {
                return this.GridContextMenu.GetClickedElement<GridViewHeaderCell>();
            }
        }
        private GridViewRow ClickedRow
        {
            get
            {
                return this.GridContextMenu.GetClickedElement<GridViewRow>();
            }
        }
        private int expertid = 0;
        public Factor(string token,int facid,int clientid=0,int Expertid=0)
        {

            _token = token;
            factorid = facid;
            ClientId = clientid;

            expertid = Expertid;

            
            InitializeComponent();
            this.txtDate.Culture = new System.Globalization.CultureInfo("fa");
            this.txtDate.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd";
            this.txtDate.SelectedDate = DateTime.Now;
            InitializeRowContextMenuItems();
            var clients = sdk.GetAllClients(_token).Result;

            if (clients.IsSuccess)
            {
                

                if (clientid!=0)
                {
                    RadComboBox1.ItemsSource = clients.Data.Where(s=>s.id== clientid).ToList();
                    RadComboBox1.SelectedItem = clients.Data.Where(s => s.id == clientid).SingleOrDefault();
                    tfactor.IsEnabled = false;
                }
                else
                {
                    RadComboBox1.ItemsSource = clients.Data;
                }
                
            }

            factorupdate();
            if (factorid!=0)
            {
               
                var factor = sdk.GetFactorById(token, factorid);
                if (factor.Result.IsSuccess)
                {
                    if (factor.Result.Data.FactorType== FactorType.Factor)
                    {
                        tfactor.IsSelected = true;
                    }
                    RadComboBox1.SelectedItem = clients.Data.Find(p => p.id == factor.Result.Data.Client_ID);
                    var rows = new System.Collections.ObjectModel.ObservableCollection<FactorAndProduct>();
                    foreach (var item in factor.Result.Data.Product_Factor)
                    {
                        var p = new FactorAndProduct();
                        p.Factor_ID = item.Factor_ID;
                        p.id = item.id;
                        p.length = item.length;
                        p.Metraj = decimal.Parse(item.Metraj, CultureInfo.InvariantCulture);
                        p.Price = item.Price;
                        p.rowDiscription = item.RowDiscription;
                        p.Unit = item.Unit;
                        p.Width = item.Width;
                        p.ProductAndService_ID = item.ProductAndService_ID;
                        rows.Add(p);
                    }
                    Rows = rows;
                    txtFactorNumber.Content = factorid;
                    txtDiscription.Text = factor.Result.Data.Discription;
                    Discount = factor.Result.Data.Discount;
                    int y, m, d;
                    string[] list = new string[3];
                    list = factor.Result.Data.DateTime.Split('/');
                    y = int.Parse(list[0]);
                    m = int.Parse(list[1]);
                    d = int.Parse(list[2]);
                    PersianCalendar pc = new PersianCalendar();
                    txtDate.SelectedDate = new DateTime(y, m, d, pc);

                }


            }
            
            bind();
            
        }
        private void bind()
        {
            if (Rows == null)
            {
                var rows = new System.Collections.ObjectModel.ObservableCollection<FactorAndProduct>();
                rows.Add(new FactorAndProduct() { ProductAndService_ID = 1 });
                Rows = rows;
            }
            dgRows.ItemsSource = Rows;
            var p = sdk.GetAllProducts(_token);
            if (p.Result.IsSuccess)
            {
                ObservableCollection<ProductAndServiceDto> list = new ObservableCollection<ProductAndServiceDto>();
                foreach (var item in p.Result.Data)
                {
                    var ppp = new ProductAndServiceDto();
                    ppp.id = item.id;
                    ppp.ProductCode = item.ProductCode;
                    ppp.ProductName = item.ProductName;
                    ppp.ProductType = item.ProductType;
                    ppp.UnitPrice = item.UnitPrice;
                    ppp.UnitType = item.UnitType;
                    list.Add(ppp);
                }
                products = list;
            }
            ((GridViewComboBoxColumn)this.dgRows.Columns[1]).ItemsSource = products;
        }
        private void CalculateRows()
        {
            var productingrid = new ObservableCollection<FactorAndProduct>();
            decimal total = 0;
            foreach (var item in dgRows.Items)
            {
                var row = item as FactorAndProduct;
                var p = sdk.GetProductByID(_token, row.ProductAndService_ID);
                if (p.Result.IsSuccess)
                {
                    switch (p.Result.Data.UnitType)
                    {
                        case UnitType.SquareMeters:
                            {
                                row.Metraj = Math.Round((row.Unit * row.length * row.Width), 3);

                            }
                            break;
                        case UnitType.Metr:
                            {
                                row.Metraj = Math.Round((row.Unit * row.length), 3);
                                row.Width = 0;
                            }
                            break;
                        case UnitType.Unit:
                            {
                                row.Metraj = Math.Round((decimal)row.Unit - 0, 0);
                                row.Width = 0;
                                row.length = 0;
                            }
                            break;
                        default:
                            break;

                    }
                    row.Price = row.Metraj * p.Result.Data.UnitPrice;
                    total += row.Price;

                    productingrid.Add(row);
                }
                
                


            }
            price = total;
            Rows = productingrid;
            dgRows.ItemsSource = Rows;

            sum.Text = total.ToString("n0");
        }
        private void factorupdate()
        {

            txtFactorNumber.Content = sdk.GetAllLastFactorNumber(_token).Result.Data + 1;

        }
        private void tab_SelectionChanged(object sender, RadSelectionChangedEventArgs e)
        {
            if (tPish.IsSelected)
            {
                //txtsample.Content = "پیش فاکتور";
                tPish.FontWeight = FontWeights.Bold;
                tPish.FontSize = 14;
                tfactor.FontWeight = FontWeights.Normal;
                tfactor.FontSize = 12;
                factorType = FactorType.PishFactor;
                if (factorid==0)
                {
                    factorupdate();
                }

                
            }
            if (tfactor.IsSelected)
            {
                //txtsample.Content = " فاکتور";
                tfactor.FontWeight = FontWeights.Bold;
                tfactor.FontSize = 14;
                tPish.FontWeight = FontWeights.Normal;
                tPish.FontSize = 12;
                factorType = FactorType.Factor;
                if (factorid == 0)
                {
                    factorupdate();
                }
            }

        }

        private void lblcodeClient_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void RadComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var client = sdk.GetClientById(_token, Convert.ToInt32(lblcodeClient.Content)).Result.Data;
            ClientId = client.id;
            if (client.DiscountPercent != 0)
            {
                DiscountPercent = client.DiscountPercent;

            }
            if (client != null)
            {
                txtphone.Text = client.ClientPhone;
                txtaddrss.Text = client.ClientAddress;
            }
            else
            {
                txtphone.Text = null;
                txtaddrss.Text = null;
            }
        }

        private void dgRows_TextChanged(object sender, TextChangedEventArgs e)
        {

            var cellnuber = dgRows.SelectedCells.SingleOrDefault().Column.DisplayIndex;
            switch (cellnuber)
            {
                case 1:
                    {
                        var productingrid = new ObservableCollection<FactorAndProduct>();
                        foreach (var item in dgRows.Items)
                        {
                            productingrid.Add(item as FactorAndProduct);
                        }

                        var rowIndex = this.dgRows.Items.IndexOf(this.dgRows.CurrentCellInfo.Item);
                        FindProduct radWindow1 = new FindProduct(_token);
                        radWindow1.Owner = this;
                        if (radWindow1.ShowDialog() == true)
                        {
                            productingrid[rowIndex].ProductAndService_ID = radWindow1.ProductID;

                        }
                        Rows = productingrid;
                        dgRows.ItemsSource = Rows;
                        CalculateRows();
                        break;
                    }

            }

            bind();
        }

        private void dgRows_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                var row = new FactorAndProduct();
                var productingrid = new ObservableCollection<FactorAndProduct>();
                foreach (var item in dgRows.Items)
                {
                    productingrid.Add(item as FactorAndProduct);
                }


                FindProduct radWindow1 = new FindProduct(_token);
                radWindow1.Owner = this;
                if (radWindow1.ShowDialog() == true)
                {
                    row.ProductAndService_ID = radWindow1.ProductID;

                }
                productingrid.Add(row);
                Rows = productingrid;
                dgRows.ItemsSource = Rows;
                CalculateRows();
                bind();

            }
        }

        private void dgRows_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            CalculateRows();
        }
        private void InitializeRowContextMenuItems()
        {
            ObservableCollection<MenuItem> items = new ObservableCollection<MenuItem>();
            MenuItem addItem = new MenuItem();
            addItem.Text = "افزودن";
            items.Add(addItem);

            MenuItem deleteItem = new MenuItem();
            deleteItem.Text = "حذف ردیف";
            items.Add(deleteItem);
            MenuItem RefreshItem = new MenuItem();
            RefreshItem.Text = "بروز رسانی";
            items.Add(RefreshItem);
            this.rowContextMenuItems = items;
        }


        private void GridContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (this.ClickedHeader != null)
            {
                foreach (var item in this.rowContextMenuItems)
                {
                    if (!item.Text.Equals("افزودن") && !item.Text.Equals("بروز رسانی"))
                    {
                        item.IsEnabled = false;
                    }

                }
                this.GridContextMenu.ItemsSource = this.rowContextMenuItems;
            }
            else if (this.ClickedRow != null)
            {
                this.dgRows.SelectedItem = this.ClickedRow.DataContext;
                foreach (var item in this.rowContextMenuItems)
                {

                    item.IsEnabled = true;
                    if (item.Text.Equals("حذف ردیف"))
                    {
                        try
                        {
                            var ob = dgRows.CurrentCellInfo.Item;
                            if (ob == null)
                            {
                                item.IsEnabled = false;
                            }
                        }
                        catch (Exception)
                        {

                            item.IsEnabled = false;
                        }

                    }
                }
                this.GridContextMenu.ItemsSource = this.rowContextMenuItems;
            }
            else
            {
                foreach (var item in this.rowContextMenuItems)
                {
                    if (!item.Text.Equals("افزودن") && !item.Text.Equals("بروز رسانی"))
                    {
                        item.IsEnabled = false;
                    }



                }
                this.GridContextMenu.ItemsSource = this.rowContextMenuItems;
            }
        }

        private void GridContextMenu_ItemClick(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            MenuItem item = (e.OriginalSource as RadMenuItem).DataContext as MenuItem;
            switch (item.Text)
            {
                case "افزودن":
                    {
                        var row = new FactorAndProduct();
                        var productingrid = new ObservableCollection<FactorAndProduct>();
                        foreach (var items in dgRows.Items)
                        {
                            productingrid.Add(items as FactorAndProduct);
                        }


                        FindProduct radWindow1 = new FindProduct(_token);
                        radWindow1.Owner = this;
                        if (radWindow1.ShowDialog() == true)
                        {
                            row.ProductAndService_ID = radWindow1.ProductID;

                        }
                        productingrid.Add(row);
                        Rows = productingrid;
                        dgRows.ItemsSource = Rows;
                        CalculateRows();
                        bind();
                    }
                    break;
                case "بروز رسانی":
                    {
                        CalculateRows();
                    }
                    break;
                case "حذف ردیف":
                    {
                        var objec = dgRows.CurrentCellInfo.Item;
                        this.dgRows.Items.Remove(objec);

                    }
                    break;

            }
        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            this.busycontent.IsBusy = true;
            CalculateRows();
            Discount discount = new Discount();
            if (ClientId == 0)
            {
                goto End;
            }

            discount.DescountPersents = Convert.ToInt32(DiscountPercent);
            if (DiscountPercent != 0)
            {
                var p = price * (DiscountPercent / 100);
                discount.Descounts = p;

            }
            else
            {
                discount.Descounts = Discount;
            }

            discount.Owner = this;
            discount.Price = price;

            if (discount.ShowDialog() == true)
            {
                Discount = discount.Descounts;
            }

            TaxtCalculate taxtCalculate = new TaxtCalculate();
            taxtCalculate.Owner = this;
            taxtCalculate.Taxt = Convert.ToInt32(Convert.ToDouble(price - Discount) * 0.09);
            taxtCalculate.IsTaxt = true;
            if (taxtCalculate.ShowDialog() == true)
            {
                IsTaxteable = taxtCalculate.IsTaxt;
            }
            AddFactorViewModel dto = new AddFactorViewModel();
            
            dto.Client_ID = ClientId;
            dto.discoun =Convert.ToInt32(Discount);
            dto.discountDefoult = false;
            dto.Discription = txtDiscription.Text;
            dto.Rasmi = IsTaxteable;
            dto.User_ID = 0;
            dto.ShamsiDate = txtDate.SelectedDate.Value.ToString("yyyy/MM/dd");
            dto.rows = new List<Product_FactorViewModel>();
            foreach (var item in dgRows.Items)
            {

                var dtoo = item as FactorAndProduct;
                var adds = new Product_FactorViewModel();
                adds.length = dtoo.length;
                adds.Unit = dtoo.Unit;
                adds.ProductAndService_ID = dtoo.ProductAndService_ID;
                adds.RowDiscription = dtoo.rowDiscription;
                adds.Width = dtoo.Width;
                adds.RowDiscription = dtoo.rowDiscription;
                
                dto.rows.Add(adds);
            }
            FactorDto factordto = null;
            if (expertid>0)
            {
                var s = sdk.AddPartnerPishFactor(_token, expertid, dto);
                if (s.Result.IsSuccess)
                {
                    factordto = s.Result.Data;
                }
            }
            else
            {
                if (factorid == 0)
                {
                    switch (factorType)
                    {
                        case FactorType.PishFactor:
                            {
                                var s = sdk.AddPishFactor(_token, dto);
                                if (s.Result.IsSuccess)
                                {
                                    factordto = s.Result.Data;
                                }
                            }
                            break;
                        case FactorType.Factor:
                            {
                                var s = sdk.AddFactor(_token, dto);
                                if (s.Result.IsSuccess)
                                {
                                    factordto = s.Result.Data;
                                }
                            }
                            break;
                        case FactorType.PendingToAccept:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (factorType)
                    {
                        case FactorType.PishFactor:
                            {
                                if (sdk.GetFactorById(_token, factorid).Result.Data.FactorType == FactorType.Factor)
                                {
                                    var re = sdk.CheangToPishFactor(_token, factorid);
                                }
                                var s = sdk.EditePishFactor(_token, factorid, dto);
                                if (s.Result.IsSuccess)
                                {
                                    factordto = s.Result.Data;
                                }
                            }
                            break;
                        case FactorType.Factor:
                            {
                                if (sdk.GetFactorById(_token, factorid).Result.Data.FactorType == FactorType.PishFactor)
                                {
                                    var re = sdk.CheangToFactor(_token, factorid);
                                }
                                var s = sdk.EditeFactor(_token, factorid, dto);
                                if (s.Result.IsSuccess)
                                {
                                    factordto = s.Result.Data;
                                }
                            }
                            break;
                        case FactorType.PendingToAccept:
                            break;
                        default:
                            break;
                    }
                }
            }
            
            
            if (factordto != null)
            {
                
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
                foreach (var item in factordto.Product_Factor)
                {

                    dataTable.Rows.Add(inss, item.ProductAndService, item.Width, item.length, item.Unit, item.Metraj, item.Price,item.RowDiscription, item.UnitPrice);
                    inss++;
                }
                var cli = sdk.GetClientById(_token, factordto.Client_ID).Result.Data;
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

                Factorinfo.Rows.Add(factordto.Client_ID, factordto.User_ID, factordto.Discount, factordto.Discription, cli.ClientName,
                   sdk.GetUserBio(_token).Result.Data.FullName, factordto.DateTime, factordto.TotalPrice, factordto.Taxes, factordto.FactorPrice, factordto.FactorCodeView,
                   factorType==FactorType.Factor?"فاکتور فروش":"پیش فاکتور", factordto.id, txtaddrss.Text,txtphone.Text);
                StiReport stiReport = new StiReport();
                stiReport.RegData("DT", dataTable);
                stiReport.RegData("FactorInfo", Factorinfo);
                string path = "";
                if (IsTaxteable)
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
                
                this.Close();
            }
           
        End:
            {
                if (ClientId == 0)
                {
                    MessageBox.Show("لطفا برای صدور فاکتور یک طرف حساب را انتخاب کنید.");
                }

            }
            busycontent.IsBusy = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RadWindow_Activated(object sender, EventArgs e)
        {
            CalculateRows();
        }
    }
}
