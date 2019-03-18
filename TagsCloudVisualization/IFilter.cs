namespace TagsCloudVisualization
{
    public interface IFilter
    {
        bool IsNecessaryPartOfSpeech(string word);
    }
}
