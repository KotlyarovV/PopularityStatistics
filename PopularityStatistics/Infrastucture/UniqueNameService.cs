using System;

namespace PopularityStatistics.Infrastucture
{
    public class UniqueNameService : IUniqueKeyService
    {
        public string GetUniqueKey(string extension) =>
            DateTime.Now.ToString().GetHashCode().ToString("x") +
            Guid.NewGuid() + extension;

        public string GetUniqueTaskKey() => DateTime.Now.ToString().GetHashCode().ToString("x") + Guid.NewGuid() + Guid.NewGuid() + Guid.NewGuid();
    }
}
