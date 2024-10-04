using CloudinaryDotNet;
using KoiDeliveryOrdering.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace KoiDeliveryOrdering.Business
{
    public class ImageService : IImageService
    {
        private readonly Account account;

        public ImageService(IConfiguration configuration)
        {
            account = new Account(configuration.GetSection("Cloundinary")["CloundName"],
                configuration.GetSection("Cloundinary")["ApiKey"],
                configuration.GetSection("Cloundinary")["ApiSecret"]);
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);
            var uploadResult = await client.UploadAsync(
                new CloudinaryDotNet.Actions.ImageUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    DisplayName = file.FileName
                });
            if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }

            return null!;
        }
    }
}
