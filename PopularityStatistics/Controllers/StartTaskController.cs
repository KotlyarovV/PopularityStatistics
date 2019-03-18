using Microsoft.AspNetCore.Mvc;
using PopularityStatistics.Models;

namespace PopularityStatistics.Controllers
{
    public class StartTaskController : Controller
    {
        private TaskRepository _taskRepository;
        public StartTaskController(TaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public IActionResult Index(ParametersModel parameters)
        {
            var startTask = _taskRepository.StartTask(parameters);
            return startTask ? View("Index") : View("Error");
        }
    }
}