using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ReportDataBase.Models
{
    public class ReportContext
    {
        IMongoDatabase database; // база данных

        public ReportContext()
        {
            var connectionString = "mongodb+srv://kotlvit:qwery@reports-qowm3.mongodb.net/test?retryWrites=true";
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            database = client.GetDatabase(connection.DatabaseName);
        }

        private IMongoCollection<Report> Reports => database.GetCollection<Report>("Reports");

        public async Task<IEnumerable<Report>> GetReports() => await Reports.Find(FilterDefinition<Report>.Empty).ToListAsync();

        private async Task<IEnumerable<Report>> GetPage(FilterDefinition<Report> filter, int pageNumber, int reportsInPage)
        {
            //Reports.AsQueryable().;
            //-1 убывание
            //1 возрастание
            SortDefinition<Report> reportSort = new JsonSortDefinition<Report>("{DateTime: -1}");
            return await Reports
                .Find(filter)
                .Skip((pageNumber - 1) * reportsInPage)
                .Limit(reportsInPage)
                .Sort(reportSort)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Report>> GetReportsPage(int pageNumber, int reportAmount) =>
            await GetPage(
                FilterDefinition<Report>.Empty, 
                pageNumber, 
                reportAmount
            ).ConfigureAwait(false);
        
        public async Task<IEnumerable<Report>> GetReportsForUserPage(string userName, int pageNumber, int reportsInPage)
        {
            FilterDefinition<Report> reportFilter = new JsonFilterDefinition<Report>($"{{FirstUserName: \"{userName}\"}}");
            return await GetPage(
                reportFilter,
                pageNumber,
                reportsInPage
            ).ConfigureAwait(false);
        }

        public async Task<Report> GetReport(string id)
        {
            return await Reports.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }
        // добавление документа
        public async Task Add(Report report)
        {
            await Reports.InsertOneAsync(report);
        }
        // обновление документа
        public async Task Update(Report report)
        {
            await Reports.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(report.Id)), report);
        }
        // удаление документа
        public async Task Remove(string id)
        {
            await Reports.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        private async Task<int> GetReportsAmount(FilterDefinition<Report> filter) => 
            (int) await Reports.CountAsync(filter);

        public async Task<int> GetReportsNumber() => await GetReportsAmount(FilterDefinition<Report>.Empty);

        public async Task<int> GetReportsAmountForUser(string user)
        {
            FilterDefinition<Report> reportFilter = new JsonFilterDefinition<Report>($"{{FirstUserName: \"{user}\"}}");
            return await GetReportsAmount(reportFilter);
        }
    }
}
