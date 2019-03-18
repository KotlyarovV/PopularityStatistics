using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization
{
    public class CloudPainter : ICloudPainter
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly IAnalysator lexicAnalysator;
        private readonly ITextVisualisator textVisualisator;
        private readonly IWordExtractor wordExtractor;
        private readonly IFormatter formatter;
        private readonly IFilter filter;

        public CloudPainter(
            IWordExtractor wordExtractor,
            IFormatter formatter,
            IFilter filter,
            IAnalysator lexicAnalysator,
            ICloudLayouter cloudLayouter,
            ITextVisualisator textVisualisator
            )
        {
            this.cloudLayouter = cloudLayouter;
            this.lexicAnalysator = lexicAnalysator;
            this.textVisualisator = textVisualisator;
            this.wordExtractor = wordExtractor;
            this.formatter = formatter;
            this.filter = filter;
        }

        private IEnumerable<TextImage> GetStringImages(
            IEnumerable<string> texts,
            Color[] colors,
            double minFont = 1.0, 
            double maxFont = 10.0, 
            string fontName = "Arial"      
            )
        {
            var listWords = new List<string>();
            var extractedWords = texts.Select(strs => wordExtractor.ExtractWords(strs)).ToList();
            foreach (var wordSet in extractedWords)
            {
                foreach (var word in wordSet)
                {
                    listWords.Add(word);
                }
            }
           

            var words = listWords
                .Where(filter.IsNecessaryPartOfSpeech)
                .Select(formatter.GetOriginal)
                .Where(str => str.Length > 1)
                .ToArray();
            var weights = lexicAnalysator.GetWeights(words);
            
            var stringImages = textVisualisator
                .CreateTextImages(weights)
                .SetFontSizes(minFont, maxFont)
                .SetColors(colors)
                .SetFontTipe(fontName)
                .GetStringImages();

            return stringImages;
        }
        
        public Bitmap GetBitmap(
            IEnumerable<string> texts, 
            Color[] colors,
            int width = 100,
            int height = 100,
            double minFont = 1.0,
            double maxFont = 10.0,
            string fontName = "Arial"
            )
        {   
            var bitmap = new Bitmap(width, height);
            var center = bitmap.Size.GetCenter();
            var graphics = Graphics.FromImage(bitmap);

            lock (this)
            {
                var textImages = GetStringImages(texts, colors, minFont, maxFont, fontName);
                textImages = textImages
                    .OrderBy(stringImage => -stringImage.Size.Width * stringImage.Size.Height);

                var flags = TextFormatFlags.NoPadding | TextFormatFlags.NoClipping;
                cloudLayouter.PrepareLayouter(center);

                foreach (var textImage in textImages)
                {
                    var rectangle = cloudLayouter.PutNextRectangle(textImage.Size);
                    TextRenderer.DrawText(
                        graphics,
                        textImage.Text,
                        textImage.Font,
                        rectangle.Location,
                        textImage.Color,
                        flags
                    );
                }
            }
            
            return bitmap;
        }

        public Task<Bitmap> GetBitmapAsync(
            IEnumerable<string> texts,
            Color[] colors,
            int width = 100,
            int height = 100,
            double minFont = 1.0,
            double maxFont = 10.0,
            string fontName = "Arial"
        ) => Task.Run(() => GetBitmap(texts, colors, width, height, minFont, maxFont, fontName));

        
    }
}
