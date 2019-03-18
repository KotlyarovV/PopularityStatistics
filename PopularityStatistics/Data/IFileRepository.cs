using System.Drawing;
using System.Threading.Tasks;

namespace PopularityStatistics.Data
{
    public interface IFileRepository
    {
        Task<string> SaveImageAsync(Bitmap image);
        Task<byte[]> GetFile(string identifictor);
        Task DeleteFile(string identifictor);
    }
}
