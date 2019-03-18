using System.Drawing;
using System.Threading.Tasks;
using CloudImageStorage;
using PopularityStatistics.Infrastucture;

namespace PopularityStatistics.Data
{
    public class CloudStorageAdapter : ImageCloudStorage, IFileRepository
    {
        private IUniqueKeyService _uniqueKeyService;

        public CloudStorageAdapter(IUniqueKeyService uniqueKeyService)
        {
            _uniqueKeyService = uniqueKeyService;
        }
        public async Task<string> SaveImageAsync(Bitmap image)
        {
            var imageName = _uniqueKeyService.GetUniqueKey(".bmp");
            await base.SaveImageAsync(image, imageName);
            return imageName;
        }
    }
}
