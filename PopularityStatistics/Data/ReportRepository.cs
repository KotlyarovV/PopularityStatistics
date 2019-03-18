using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportDataBase.Models;
using Report = PopularityStatistics.Models.Report;

namespace PopularityStatistics.Data
{
    public class ReportRepository : IReportRepository
    {
        private ReportContext _reportContext;

        public ReportRepository(ReportContext reportContext)
        {
            _reportContext = reportContext;
        }
        
        public Task SaveReport(Report report)
        {
            return _reportContext.Add((ReportDataBase.Models.Report) report);
        }

        public async Task<IEnumerable<Report>> GetReports()
        {
            var reportsFromDataBase = await _reportContext.GetReports();
            return reportsFromDataBase.Select(ConvertDatabaseReport);
        }

        public async Task<IEnumerable<Report>> GetReportsPage(int pageNumber, int reportCount)
        {
            var reportsFromBase = await _reportContext.GetReportsPage(pageNumber, reportCount);
            return reportsFromBase.Select(ConvertDatabaseReport);
        }

        public async Task<IEnumerable<Report>> GetReportsPageForUser(int pageNumber, int reportAmount, string user)
        {
            var reportsFromBase = await _reportContext.GetReportsForUserPage(user, pageNumber, reportAmount);
            return reportsFromBase.Select(ConvertDatabaseReport);
        }

        private int GetPageCount(int reportsCount, int reportsOnPage) => reportsCount / reportsOnPage + (reportsCount % reportsOnPage == 0 ? 0 : 1);

        public async Task<int> GetPagesNumber(int reportsOnPage)
        {
            var reportsCount = await _reportContext.GetReportsNumber();
            return GetPageCount(reportsCount, reportsOnPage);
        }

        public async Task<int> GetPagesNumberForUser(int reportsOnPage, string user)
        {
            var reportsCount = await _reportContext.GetReportsAmountForUser(user);
            return GetPageCount(reportsCount, reportsOnPage);
        }

        private static Report ConvertDatabaseReport(ReportDataBase.Models.Report reportDataBase)
        {
            var report = new Report
            {
                AgeReport = reportDataBase.AgeReport,
                DateTime = reportDataBase.DateTime,
                ErrorMessage = reportDataBase.ErrorMessage,
                FirstUserName = reportDataBase.FirstUserName,
                IsFailed = reportDataBase.IsFailed,
                SexReport = reportDataBase.SexReport,
                Theme = reportDataBase.Theme,
                WayToFile = reportDataBase.WayToFile
            };
            return report;
        }
    }
}
