using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public interface ICloudPainter
    {
        Bitmap GetBitmap(
            IEnumerable<string> data,
            Color[] colors,
            int width = 100,
            int height = 100,
            double minFont = 1.0,
            double maxFont = 10.0,
            string fontName = "Arial"
        );

        Task<Bitmap> GetBitmapAsync(
            IEnumerable<string> text,
            Color[] colors,
            int width = 100,
            int height = 100,
            double minFont = 1.0,
            double maxFont = 10.0,
            string fontName = "Arial"
        );
    }
}
