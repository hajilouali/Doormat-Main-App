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
    public static class SanadsReports
    {
        public static void SanadsView(List<SanadsReportsViewModel> SanadsReportsViewModel)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("AccountingHeading_ID");
            dataTable.Columns.Add("Discription");
            dataTable.Columns.Add("Sanad_ID");
            dataTable.Columns.Add("Bedehkari");
            dataTable.Columns.Add("Bestankari");
            dataTable.Columns.Add("Comment");
            dataTable.Columns.Add("DateTime");
            dataTable.Columns.Add("AccountingHeading");
            foreach (var item in SanadsReportsViewModel)
            {
                dataTable.Rows.Add(item.AccountingHeading_ID, item.Discription, item.Sanad_ID, item.Bedehkari, item.Bestankari, item.Comment, item.DateTime, item.AccountingHeading);
            }
            StiReport stiReport = new StiReport();
            stiReport.RegData("DT", dataTable);
            var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                       + "\\ReportsForm\\Sanads.mrt";
            stiReport.Load(path);
            stiReport.Show();
        }
    }
}
