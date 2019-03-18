using System.Drawing;

namespace PopularityStatistics.Models
{
    public class ParametersModel
    {
        public string User { get; set; }
        public FilterEnum Filter { get; set; }
        public SexEnum Sex { get; set; }
        public Color[] Colors { get; set; }
        public int MinFont { get; set; }
        public int MaxFont { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string FontName { get; set; }
        public int FromAge { get; set; }
        public int ToAge { get; set; }
        public bool AgeIsNotImportant { get; set; }
    }
}
