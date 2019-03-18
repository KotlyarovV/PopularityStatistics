using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace PopularityStatistics.Infrastucture
{
    public class TaskPool<T>
    {
        private ConcurrentDictionary<string, Task<T>> _taskBag = new ConcurrentDictionary<string, Task<T>>();
        private IUniqueKeyService _uniqueKeyService;

        public TaskPool(IUniqueKeyService uniqueKeyService)
        {
            _uniqueKeyService = uniqueKeyService;
        }
        private async Task<T> GetTask(string key, Task<T> task)
        {
            await task;
            _taskBag.TryRemove(key, out _);
            return task.Result;
        }

        public bool TryAddTask(Task<T> task)
        {
            var key = _uniqueKeyService.GetUniqueTaskKey();
            var taskLocal = GetTask(key, task);
            return _taskBag.TryAdd(key, taskLocal);
        }
    }
}
