using System.ComponentModel;

namespace PopularityStatistics.Models
{
    public enum FilterEnum
    {
        [Description("Книги")]
        Books,

        [Description("Кино")]
        Movies,

        [Description("ТВ передачи")]
        TV,

        [Description("Музыка")]
        Music
    }
}
