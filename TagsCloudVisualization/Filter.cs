using System.Collections.Generic;
using System.Linq;
using YandexMystem.Wrapper.Enums;


namespace TagsCloudVisualization
{
    public class Filter : IFilter
    {
        private static readonly char[] ForbiddenChars = { '_', '#' };

        public bool IsNecessaryPartOfSpeech(string word)
        {
            return !ForbiddenChars.Any(word.Contains);
        }
    }
}
