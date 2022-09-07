using SiteNews.Web.Models.Images;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;

namespace SiteNews.Web.Services
{
    public class ImageService : IImageService
    {
        private const int ThumbnailWidth = 300;
        private const int FullScreenWidth = 640;

        public void Process(IEnumerable<ImageInputModel> images)
        {
            foreach (var image in images)
            {
                using var imageResult = Image.Load(image.Content);
                var width = imageResult.Width;
                var height = imageResult.Height;
                if (width > FullScreenWidth)
                {
                    height = FullScreenWidth / width * height;
                    width = FullScreenWidth;
                }
                imageResult
                    .Mutate(i => i
                    .Resize(new Size(width, height)));

                imageResult.Metadata.ExifProfile = null;

                imageResult.SaveAsJpeg("wwwroot/images/haberler/" + image.FileName, new JpegEncoder
                {
                    Quality = 60
                });
            }
        }
        public void ProcessGallery(IEnumerable<ImageInputModel> images)
        {
            foreach (var image in images)
            {
                using var imageResult = Image.Load(image.Content);
                var width = imageResult.Width;
                var height = imageResult.Height;
                if (width > FullScreenWidth)
                {
                    height = FullScreenWidth / width * height;
                    width = FullScreenWidth;
                }
                imageResult
                    .Mutate(i => i
                    .Resize(new Size(width, height)));

                imageResult.Metadata.ExifProfile = null;

                imageResult.SaveAsJpeg("wwwroot/images/galeri/" + image.FileName, new JpegEncoder
                {
                    Quality = 60
                });
            }
        }
    }
}
