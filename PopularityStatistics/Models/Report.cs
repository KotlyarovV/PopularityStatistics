using System;
using PopularityStatistics.Infrastucture;

namespace PopularityStatistics.Models
{
    public class Report
    {
        public string Theme { get; set; }
        public string AgeReport { get; set; }
        public string SexReport { get; set; }
        public string FirstUserName { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsFailed { get; set; }
        public string ErrorMessage { get; set; }

        public Report()
        {
        }

        public Report(ParametersModel param)
        {
            AgeReport = param.AgeIsNotImportant ? "не важен" : $"от {param.FromAge} до {param.ToAge} лет";
            FirstUserName = param.User;
            DateTime = DateTime.Now;
            SexReport = param.Sex.Description();
            Theme = param.Filter.Description();
        }

        public string WayToFile { get; set; }
        public void SetWayToFileWithSuccess(string way)
        {
            ErrorMessage = "OK";
            WayToFile = way;
        }
        
        public ErrorEnum Error
        {
            set
            {
                IsFailed = true;
                ErrorMessage = value.Description();
            }
        }

        public static explicit operator ReportDataBase.Models.Report(Report report)
        {
            var dataBaseReport = new ReportDataBase.Models.Report();
            dataBaseReport.AgeReport = report.AgeReport;
            dataBaseReport.DateTime = report.DateTime;
            dataBaseReport.ErrorMessage = report.ErrorMessage;
            dataBaseReport.FirstUserName = report.FirstUserName;
            dataBaseReport.IsFailed = report.IsFailed;
            dataBaseReport.SexReport = report.SexReport;
            dataBaseReport.WayToFile = report.WayToFile;
            dataBaseReport.Theme = report.Theme;
            return dataBaseReport;
        }
    }
}
