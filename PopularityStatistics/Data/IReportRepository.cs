using System.Collections.Generic;
using System.Threading.Tasks;
using PopularityStatistics.Models;

namespace PopularityStatistics.Data
{
    public interface IReportRepository
    {
        Task SaveReport(Report report);
        Task<IEnumerable<Report>> GetReports();
        Task<IEnumerable<Report>> GetReportsPage(int pageNumber, int reportAmount);
        Task<IEnumerable<Report>> GetReportsPageForUser(int pageNumber, int reportAmount, string user);
        Task<int> GetPagesNumber(int reportsOnPage);
        Task<int> GetPagesNumberForUser(int reportsOnPage, string user);
    }
}
