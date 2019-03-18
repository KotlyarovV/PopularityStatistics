using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWordExtractor
    {
        IEnumerable<string> ExtractWords(string text);
    }
}
