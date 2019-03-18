using System.ComponentModel;

namespace PopularityStatistics.Models
{
    public enum SexEnum
    {
        [Description("не важен")]
        Both,

        [Description("М")]
        Man,

        [Description("Ж")]
        Woman
    }
}
