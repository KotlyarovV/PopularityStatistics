using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PopularityStatistics.Data;
using PopularityStatistics.Models;

namespace PopularityStatistics.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {     
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
