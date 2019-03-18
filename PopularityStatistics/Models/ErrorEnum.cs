using System.ComponentModel;

namespace PopularityStatistics.Models
{
    public enum ErrorEnum
    {
        [Description("Ошибка при формировании облака")]
        CloudError,

        [Description("Ошибка при получении данных с VK API")]
        GetDataError,

        [Description("Не оказалось данных для построения облака")]
        EmptyDataError,

        [Description("Не удалось сохранить картинку статистики")]
        SaveError
    }
}
