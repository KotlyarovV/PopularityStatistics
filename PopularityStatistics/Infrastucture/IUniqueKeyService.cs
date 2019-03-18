namespace PopularityStatistics.Infrastucture
{
    public interface IUniqueKeyService
    {
        string GetUniqueKey(string extension);

        string GetUniqueTaskKey();
    }
}
