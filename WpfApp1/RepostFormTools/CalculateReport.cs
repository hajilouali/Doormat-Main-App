using DoormatSite.Tools;
using PhoenixFutureApiSdk;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.RepostFormTools
{
    public class CalculateReport
    {
        private string _token;
        FutureSdk sdk = new FutureSdk(xml.loadline("serverApi"));
        public CalculateReport(string token)
        {
            _token = token;
        }
        public void yearshop(int year)
        {
            if (year > 1300 && year < 1600)
            {
                var respons = sdk.getyearsshop(_token, year);
                if (respons.Result.IsSuccess)
                {
                    DataTable dataTable = new DataTable();

                    dataTable.Columns.Add("Id");
                    dataTable.Columns.Add("Value");
                    dataTable.Columns.Add("Name");
                    foreach (var item in respons.Result.Data)
                    {
                        dataTable.Rows.Add(item.id, item.Value, item.Name);
                    }
                    StiReport stiReport = new StiReport();
                    stiReport.RegData("DT", dataTable);
                    var path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                         + "\\ReportsForm\\Report.mrt";
                    stiReport.Load(path);
                    stiReport.Show();
                }
            }
        }
    }
}
