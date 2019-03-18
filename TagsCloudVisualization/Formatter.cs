using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class Formatter : IFormatter
    {
        private static readonly HashSet<char> PossibleAuthorChars = new HashSet<char> { '-', ':' };

        public string GetOriginal(string word)
        {
            return string.Concat(word
                .Select(letter =>
                {
                    char result = letter;
                    if (PossibleAuthorChars.Contains(letter))
                    {
                        result = '\0';
                    }

                    return result;
                }))
                .Trim()
                .ToLower();
        }
    }
}
