using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace CloudImageStorage
{
    public class ImageCloudStorage
    {
        private readonly string _bucketName = "popularitystatistics";
        private readonly StorageClient _storageClient;

        public ImageCloudStorage()
        {
            var credential = GoogleCredential.FromFile("PopularityStatistics-cb0e0fe38381.json");
            _storageClient = StorageClient.Create(credential);
        }

        public async Task<string> SaveImageAsync(Bitmap image, string key)
        {
            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Bmp);
            var imageAcl = PredefinedObjectAcl.PublicRead;

            var imageObject = await _storageClient.UploadObjectAsync(
                bucket: _bucketName,
                objectName: key,
                contentType: "image/bmp",
                source: ms,
                options: new UploadObjectOptions { PredefinedAcl = imageAcl }
            );
            return imageObject.MediaLink;
        }

        public async Task<byte[]> GetFile(string identificator)
        {
            var stream = new MemoryStream();
            await _storageClient.DownloadObjectAsync(
                bucket: _bucketName,
                objectName: identificator,
                destination: stream
            );
            return stream.ToArray();
        }

        public async Task DeleteFile(string identificator)
        {
            var task = _storageClient.DeleteObjectAsync(
                bucket: _bucketName,
                objectName: identificator
            );
            await Task.WhenAny(task);
        }
    }
}
