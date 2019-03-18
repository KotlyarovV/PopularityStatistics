using System.Collections.Generic;
using System.Linq;
using YandexMystem.Wrapper;

namespace TagsCloudVisualization
{
    public class WordExtractor : IWordExtractor
    {
        private static int MAX_ITEM_LENGH = 29;
        private static readonly char[] Separators = { ',', '.', ';' };
        private static readonly char[] ForbiddenChars = { '_', '#' };
        private static readonly HashSet<char> Quotes = new HashSet<char> { '"', '\'' };

        public IEnumerable<string> ExtractWords(string text)
        {


            bool isQuoteFormat = false;
            List<string> quoteResult = new List<string>();
            for (int i = 0; i < text.Length; i++)
            {
                if (Quotes.Contains(text[i]))
                {
                    isQuoteFormat = true;
                    var startIndex = ++i;
                    while (i < text.Length && !Quotes.Contains(text[i]))
                    {
                        if (ForbiddenChars.Contains(text[i]) || Separators.Contains(text[i]))
                            text = text.Replace(text[i], '\0');
                        i++;
                    }

                    quoteResult.Add(text
                        .Substring(startIndex, i - startIndex)
                        .Trim()
                        .ToLower());
                }
            }
            if (isQuoteFormat)
            {
                return quoteResult;
            }



            int maxCount = -1;
            string[] result = null;

            foreach (var separator in Separators)
            {
                string[] splitedText = text.Split(separator);
                // чем больше слов (книг) получили от сепаратора, тем операратор более подходит
                if (splitedText.Length > maxCount)
                {
                    maxCount = splitedText.Length;
                    result = splitedText;
                }
            }
            return result
                .Where(sequence => sequence.Length < MAX_ITEM_LENGH);
        }
    }
}
