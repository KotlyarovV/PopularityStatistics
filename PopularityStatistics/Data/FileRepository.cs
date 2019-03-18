using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using PopularityStatistics.Infrastucture;

namespace PopularityStatistics.Data
{
    public class FileRepository : IFileRepository
    {
        private const string FileStorage = "files";
        private const string ImageStorage = "images";

        private readonly IUniqueKeyService _uniqueNameService;

        public FileRepository(IUniqueKeyService uniqueNameService)
        {
            _uniqueNameService = uniqueNameService;
        }

        public string SaveImage(Bitmap image)
        {
            return SaveFileInStorage(ImageStorage, image);
        }

        public async Task<string> SaveImageAsync(Bitmap image)
        {
            return await Task.Run(() => SaveImage(image));
        }

        public async Task<byte[]> GetFile(string fileName)
        {
            byte[] file;
            using (var fileStream = File.Open(fileName, FileMode.Open))
            {
                file = new byte[fileStream.Length];
                await fileStream.ReadAsync(file, 0, (int)fileStream.Length);
            }

            return file;
        }

        public async Task DeleteFile(string identifictor)
        {
            using (FileStream stream = new FileStream(
                identifictor,
                FileMode.Truncate,
                FileAccess.Write,
                FileShare.Delete,
                4096, true))
            {
                await stream.FlushAsync();
                File.Delete(identifictor);
            }
        }

        private string SaveFileInStorage(string storage, Bitmap file)
        {
            if (!Directory.Exists(storage))
            {
                Directory.CreateDirectory(storage);
            }

            var nameInStorage = _uniqueNameService.GetUniqueKey(".bmp");
            var wayToFile = Path.Combine(storage, nameInStorage);

            using (var stream = new FileStream(wayToFile, FileMode.Create))
            {
                file.Save(stream, ImageFormat.Bmp);
            }
            return wayToFile;
        }
    }
}
