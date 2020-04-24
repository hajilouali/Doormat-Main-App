using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.RepostFormTools.ViewModel;

namespace WpfApp1.RepostFormTools
{
    public static class MoeinReport
    {
        public static void ClientMoein(List<ClientReportsViewModel> model)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ClientName");
            dataTable.Columns.Add("ClientID");
            dataTable.Columns.Add("ClientAddress");
            dataTable.Columns.Add("ClientPhone");
            dataTable.Columns.Add("tbRow");
            dataTable.Columns.Add("tbSanadID");
            dataTable.Columns.Add("tbDate");
            dataTable.Columns.Add("tbDiscription");
            dataTable.Columns.Add("tbBedehkar");
            dataTable.Columns.Add("tbBestankar");
            dataTable.Columns.Add("ReportDate");

            foreach (var item in model)
            {
                dataTable.Rows.Add(item.ClientName,item.ClientID,item.ClientAddress,item.ClientPhone,item.tbRow,
                    item.tbSanadID,item.tbDate,item.tbDiscription,item.tbBedehkar,item.tbBestankar,item.ReportDate);
            }
            StiReport stiReport = new StiReport();
            stiReport.RegData("DT", dataTable);
            var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                       + "\\ReportsForm\\MoeinClient.mrt";
            stiReport.Load(path);
            stiReport.Show();
        }
        public static void Moein(List<ClientReportsViewModel> model)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ClientName");
            dataTable.Columns.Add("ClientID");
            dataTable.Columns.Add("ClientAddress");
            dataTable.Columns.Add("ClientPhone");
            dataTable.Columns.Add("tbRow");
            dataTable.Columns.Add("tbSanadID");
            dataTable.Columns.Add("tbDate");
            dataTable.Columns.Add("tbDiscription");
            dataTable.Columns.Add("tbBedehkar");
            dataTable.Columns.Add("tbBestankar");
            dataTable.Columns.Add("ReportDate");

            foreach (var item in model)
            {
                dataTable.Rows.Add(item.ClientName, item.ClientID, item.ClientAddress, item.ClientPhone, item.tbRow,
                    item.tbSanadID, item.tbDate, item.tbDiscription, item.tbBedehkar, item.tbBestankar, item.ReportDate);
            }
            StiReport stiReport = new StiReport();
            stiReport.RegData("DT", dataTable);
            var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                       + "\\ReportsForm\\Moein.mrt";
            stiReport.Load(path);
            stiReport.Show();
        }
        public static void MoeinDaftar(List<ClientReportsViewModel> model)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ClientName");
            dataTable.Columns.Add("ClientID");
            dataTable.Columns.Add("ClientAddress");
            dataTable.Columns.Add("ClientPhone");
            dataTable.Columns.Add("tbRow");
            dataTable.Columns.Add("tbSanadID");
            dataTable.Columns.Add("tbDate");
            dataTable.Columns.Add("tbDiscription");
            dataTable.Columns.Add("tbBedehkar");
            dataTable.Columns.Add("tbBestankar");
            dataTable.Columns.Add("ReportDate");

            foreach (var item in model)
            {
                dataTable.Rows.Add(item.ClientName, item.ClientID, item.ClientAddress, item.ClientPhone, item.tbRow,
                    item.tbSanadID, item.tbDate, item.tbDiscription, item.tbBedehkar, item.tbBestankar, item.ReportDate);
            }
            StiReport stiReport = new StiReport();
            stiReport.RegData("DT", dataTable);
            var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                       + "\\ReportsForm\\MoeinDaftar.mrt";
            stiReport.Load(path);
            stiReport.Show();
        }
        public static void MoeinAccountingCleints(List<ClientReportsViewModel> model)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ClientName");
            dataTable.Columns.Add("ClientID");
            dataTable.Columns.Add("ClientAddress");
            dataTable.Columns.Add("ClientPhone");
            dataTable.Columns.Add("tbRow");
            dataTable.Columns.Add("tbSanadID");
            dataTable.Columns.Add("tbDate");
            dataTable.Columns.Add("tbDiscription");
            dataTable.Columns.Add("tbBedehkar");
            dataTable.Columns.Add("tbBestankar");
            dataTable.Columns.Add("ReportDate");

            foreach (var item in model)
            {
                dataTable.Rows.Add(item.ClientName, item.ClientID, item.ClientAddress, item.ClientPhone, item.tbRow,
                    item.tbSanadID, item.tbDate, item.tbDiscription, item.tbBedehkar, item.tbBestankar, item.ReportDate);
            }
            StiReport stiReport = new StiReport();
            stiReport.RegData("DT", dataTable);
            var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                       + "\\ReportsForm\\MoeinBedehkariBestankari.mrt";
            stiReport.Load(path);
            stiReport.Show();
        }
    }
}
