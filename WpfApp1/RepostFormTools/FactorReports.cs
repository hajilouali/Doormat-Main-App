using DataLayer.Api.Response;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.RepostFormTools
{
    
    public static class FactorReports
    {
        public static void FactorViewReport(FactorDto dto,ClientDto client)
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
            foreach (var item in dto.Product_Factor)
            {

                dataTable.Rows.Add(inss, item.ProductAndService, item.Width, item.length, item.Unit, item.Metraj, item.Price, item.RowDiscription, item.UnitPrice);
                inss++;
            }
            var cli = client;
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

            Factorinfo.Rows.Add(dto.Client_ID, dto.User_ID, dto.Discount, dto.Discription, cli.ClientName,
               dto.UserName, dto.DateTime, dto.TotalPrice, dto.Taxes, dto.FactorPrice, dto.FactorCodeView,
               dto.FactorType == FactorType.Factor ? "فاکتور فروش" : "پیش فاکتور", dto.id, cli.ClientAddress, cli.ClientPhone);
            StiReport stiReport = new StiReport();
            stiReport.RegData("DT", dataTable);
            stiReport.RegData("FactorInfo", Factorinfo);
            string path = "";
            if (dto.Taxes!=0)
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
        public static void HavalehViewReport(FactorDto dto, ClientDto client)
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
            foreach (var item in dto.Product_Factor)
            {

                dataTable.Rows.Add(inss, item.ProductAndService, item.Width, item.length, item.Unit, item.Metraj, item.Price, item.RowDiscription, item.UnitPrice);
                inss++;
            }
            var cli = client;
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

            Factorinfo.Rows.Add(dto.Client_ID, dto.User_ID, dto.Discount, dto.Discription, cli.ClientName,
               dto.UserName, dto.DateTime, dto.TotalPrice, dto.Taxes, dto.FactorPrice, dto.FactorCodeView,
               dto.FactorType == FactorType.Factor ? "فاکتور فروش" : "پیش فاکتور", dto.id, cli.ClientAddress, cli.ClientPhone);
            StiReport stiReport = new StiReport();
            stiReport.RegData("DT", dataTable);
            stiReport.RegData("FactorInfo", Factorinfo);
            string path = "";
            path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                       + "\\ReportsForm\\HavalehAnbar.mrt";


            stiReport.Load(path);
            stiReport.Show();
        }
    }
}
