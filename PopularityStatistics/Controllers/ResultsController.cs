using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PopularityStatistics.Data;
using PopularityStatistics.Models;

namespace PopularityStatistics.Controllers
{
    public class ResultsController : Controller
    {
        private IReportRepository _reportRepository;

        public ResultsController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [NonAction]
        private async Task<IActionResult> GetPage(int index, Task<int> pageNumbersTask, Task<IEnumerable<Report>> reportsTask)
        {
            ViewData["cp"] = index;
            if (index > 1)
            {
                ViewData["pp"] = index - 1;
            }
            await pageNumbersTask;
            var pageNumber = pageNumbersTask.Result;
            if (index < pageNumber)
            {
                ViewData["cp+1"] = index + 1;
            }
            if (index + 1 < pageNumber)
            {
                ViewData["cp+2"] = index + 2;
            }
            if (index + 2 < pageNumber)
            {
                ViewData["np"] = index + 3;
            }
            await reportsTask;
            
            return (!reportsTask.Result.Any()) ? View("NoResult") : View("Results", reportsTask.Result);
        }

        [Route("[controller]/results/{index}")]
        public async Task<IActionResult> Results(int index)
        {
            var pageNumbersTask = _reportRepository.GetPagesNumber(20);
            var reportsTask = _reportRepository.GetReportsPage(index, 20);
            return await GetPage(index, pageNumbersTask, reportsTask);
        }

        [Route("[controller]/results/{user}/{index}")]
        public async Task<IActionResult> Results(int index, string user)
        {
            var pageNumberTask = _reportRepository.GetPagesNumberForUser(20, user);
            var reportsTask = _reportRepository.GetReportsPageForUser(index, 20, user);
            return await GetPage(index, pageNumberTask, reportsTask);
        }

        [Route("[controller]/search/{index}")]
        public async Task<IActionResult> Search(string search, int index)
        {
            return Redirect($"/results/results/{search}/{index}");
        }
    }
}